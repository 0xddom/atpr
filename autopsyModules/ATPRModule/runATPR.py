# Sample module in the public domain. Feel free to use this as a template
# for your modules (and you can remove this header and take complete credit
# and liability)

import jarray
import inspect
import os
import subprocess
from java.lang import Class
from java.lang import System
from java.util.logging import Level
from org.sleuthkit.datamodel import SleuthkitCase
from org.sleuthkit.datamodel import AbstractFile
from org.sleuthkit.datamodel import ReadContentInputStream
from org.sleuthkit.datamodel import BlackboardArtifact
from org.sleuthkit.datamodel import BlackboardAttribute
from org.sleuthkit.autopsy.ingest import IngestModule
from org.sleuthkit.autopsy.ingest.IngestModule import IngestModuleException
from org.sleuthkit.autopsy.ingest import DataSourceIngestModule
from org.sleuthkit.autopsy.ingest import IngestModuleFactoryAdapter
from org.sleuthkit.autopsy.ingest import IngestMessage
from org.sleuthkit.autopsy.ingest import IngestServices
from org.sleuthkit.autopsy.ingest import ModuleDataEvent
from org.sleuthkit.autopsy.coreutils import Logger
from org.sleuthkit.autopsy.coreutils import PlatformUtil
from org.sleuthkit.autopsy.casemodule import Case
from org.sleuthkit.autopsy.casemodule.services import Services
from org.sleuthkit.autopsy.datamodel import ContentUtils


# Factory that defines the name and details of the module and allows Autopsy
# to create instances of the modules that will do the analysis.
class RunExeIngestModuleFactory(IngestModuleFactoryAdapter):

    moduleName = "Run EXE Module"

    def getModuleDisplayName(self):
        return self.moduleName

    def getModuleDescription(self):
        return "Sample module that runs img_stat on each disk image."

    def getModuleVersionNumber(self):
        return "1.0"

    def isDataSourceIngestModuleFactory(self):
        return True

    def createDataSourceIngestModule(self, ingestOptions):
        return RunExeIngestModule()


# Data Source-level ingest module.  One gets created per data source.
class RunExeIngestModule(DataSourceIngestModule):

    _logger = Logger.getLogger(RunExeIngestModuleFactory.moduleName)

    def log(self, level, msg):
        self._logger.logp(level, self.__class__.__name__, inspect.stack()[1][3], msg)

    def __init__(self):
        self.context = None

    # Where any setup and configuration is done
    # 'context' is an instance of org.sleuthkit.autopsy.ingest.IngestJobContext.
    # See: http://sleuthkit.org/autopsy/docs/api-docs/3.1/classorg_1_1sleuthkit_1_1autopsy_1_1ingest_1_1_ingest_job_context.html
    def startUp(self, context):
        self.context = context
        
        # Get path to EXE based on where this script is run from.
        # Assumes EXE is in same folder as script
        # Verify it is there before any ingest starts
        self.path_to_exe = os.path.join(os.path.dirname(os.path.abspath(__file__)), "atpr.exe")
        if not os.path.exists(self.path_to_exe):
            raise IngestModuleException("EXE was not found in module folder")

        self.path_to_dirs = os.path.join(os.path.dirname(os.path.abspath(__file__)), "/dicts/")
        if not os.path.exists(self.path_to_dirs):
            raise IngestModuleException("EXE was not found in module folder")

    # Where the analysis is done.
    # The 'dataSource' object being passed in is of type org.sleuthkit.datamodel.Content.
    # See: http://www.sleuthkit.org/sleuthkit/docs/jni-docs/4.3/interfaceorg_1_1sleuthkit_1_1datamodel_1_1_content.html
    # 'progressBar' is of type org.sleuthkit.autopsy.ingest.DataSourceIngestModuleProgress
    # See: http://sleuthkit.org/autopsy/docs/api-docs/3.1/classorg_1_1sleuthkit_1_1autopsy_1_1ingest_1_1_data_source_ingest_module_progress.html
    def process(self, dataSource, progressBar):
        
        # we don't know how much work there will be
        progressBar.switchToIndeterminate()

        # Get the folder with de texts files            
        inputDir = Case.getCurrentCase().getModulesOutputDirAbsPath() + "\TextFiles"
        try:
            os.mkdir(inputDir)
            self.log(Level.INFO, "Find Text Directory must exists for launching the module" + inputDir)
        except:
            self.log(Level.INFO, "Find Text Directory exists " + inputDir)

        # We'll save our output to a file in the reports folder, named based on EXE and data source ID
        reportPath = os.path.join(Case.getCurrentCase().getCaseDirectory(), "Reports", "atprResult-" + str(dataSource.getId()) + ".csv") 
       
        logPath = os.path.join(Case.getCurrentCase().getCaseDirectory(), "Reports", "log" + str(dataSource.getId()) + ".txt") 
        logHandle = open(reportPath, 'w')

        # Run the EXE, saving output to the report
        self.log(Level.INFO, "Running program on data source")
        subprocess.Popen([self.path_to_exe, "-c", "match" , "-i", inputDir, "-d", self.path_to_dirs, "-o", reportPath], stdout=logHandle).communicate()[0]    
        logHandle.close()
        
        # Add the report to the case, so it shows up in the tree
        Case.getCurrentCase().addReport(reportPath, "Run EXE", "ATPR result output")
        
        return IngestModule.ProcessResult.OK