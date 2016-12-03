#!/usr/bin/env python
# -*- coding: utf-8 -*-

import jarray
import inspect
import os
import subprocess
import codecs
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

    moduleName = "Advanced Text Pattern Recognition Module"

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
        absolute_path = os.path.dirname(os.path.abspath(__file__))
        
        self.path_to_exe = absolute_path + "\\atpr_exe\\ATPR.exe"
        self.log(Level.INFO, self.path_to_exe)
        #self.path_to_exe = os.path.join(os.path.dirname(os.path.abspath(__file__)), "\\atpr_exe\\ATPR.exe")
        
        if not os.path.exists(self.path_to_exe):
            raise IngestModuleException("EXE was not found in module folder")

        #self.path_to_dirs = os.path.join(os.path.dirname(os.path.abspath(__file__)), "\\dicts\\")
        self.path_to_dirs = absolute_path + "\\dicts\\"
        if not os.path.exists(self.path_to_dirs):
            raise IngestModuleException("Dicts were not found in module folder")

    # Where the analysis is done.
    # The 'dataSource' object being passed in is of type org.sleuthkit.datamodel.Content.
    # See: http://www.sleuthkit.org/sleuthkit/docs/jni-docs/4.3/interfaceorg_1_1sleuthkit_1_1datamodel_1_1_content.html
    # 'progressBar' is of type org.sleuthkit.autopsy.ingest.DataSourceIngestModuleProgress
    # See: http://sleuthkit.org/autopsy/docs/api-docs/3.1/classorg_1_1sleuthkit_1_1autopsy_1_1ingest_1_1_data_source_ingest_module_progress.html
    def process(self, dataSource, progressBar):
        
        skCase = Case.getCurrentCase().getSleuthkitCase();

        #Create all the variables needed for show them in the autopsy interface
        self.log(Level.INFO, "Begin Create New Artifacts")
        try:
            artID_eu = skCase.addArtifactType( "TSK_ATPR", "ATPR Results")
        except:     
            self.log(Level.INFO, "Artifacts Creation Error, ID ==> ")

        try:
            attID_filePath = skCase.addArtifactAttributeType("TSK_FILE_PATH", BlackboardAttribute.TSK_BLACKBOARD_ATTRIBUTE_VALUE_TYPE.STRING, "File Path")
        except:     
            self.log(Level.INFO, "Attributes Creation Error, File path ==> ")

        try:
            attID_match = skCase.addArtifactAttributeType("TSK_WORD", BlackboardAttribute.TSK_BLACKBOARD_ATTRIBUTE_VALUE_TYPE.STRING, "Word")
        except:     
            self.log(Level.INFO, "Attributes Creation Error, Word ==> ")

        try:
            attTotal_match = skCase.addArtifactAttributeType("TSK_TOTAL", BlackboardAttribute.TSK_BLACKBOARD_ATTRIBUTE_VALUE_TYPE.STRING, "Total")
        except:     
            self.log(Level.INFO, "Attributes Creation Error, Total ==> ")

        try:
            attType_match = skCase.addArtifactAttributeType("TSK_TYPE", BlackboardAttribute.TSK_BLACKBOARD_ATTRIBUTE_VALUE_TYPE.STRING, "Type")
        except:     
            self.log(Level.INFO, "Attributes Creation Error, Type ==> ")

        try:
            attDict_match = skCase.addArtifactAttributeType("TSK_DICT", BlackboardAttribute.TSK_BLACKBOARD_ATTRIBUTE_VALUE_TYPE.STRING, "Dict")
        except:     
            self.log(Level.INFO, "Attributes Creation Error, Dict ==> ")

        artID_eu = skCase.getArtifactTypeID("TSK_ATPR")
        artID_eu_evt = skCase.getArtifactType("TSK_ATPR")
        attID_fp = skCase.getAttributeType("TSK_FILE_PATH")
        attID_match = skCase.getAttributeType("TSK_WORD")
        attTotal_match = skCase.getAttributeType("TSK_TOTAL")
        attType_match = skCase.getAttributeType("TSK_TYPE")
        attDict_match = skCase.getAttributeType("TSK_DICT")

        # we don't know how much work there will be
        progressBar.switchToIndeterminate()

        # Get the folder with de texts files            
        inputDir = Case.getCurrentCase().getModulesOutputDirAbsPath() + "\TextFiles"

        try:
            os.makesdirs(inputDir)
            self.log(Level.INFO, "Find Text Directory must exists for launching the module" + inputDir)
        except:
            self.log(Level.INFO, "Find Text Directory exists " + inputDir)

        # We'll save our output to a file in the reports folder, named based on EXE and data source ID
        reportPath = Case.getCurrentCase().getCaseDirectory() + "\\Reports\\atprResult-" + str(dataSource.getId()) + ".csv" 

        #reportPath = os.path.join(Case.getCurrentCase().getTempDirectory(), str(dataSource.getId()))
        logPath = Case.getCurrentCase().getCaseDirectory() + "\\Reports\\log" + str(dataSource.getId()) + ".txt"
        logHandle = open(logPath, 'w')

        # Run the EXE, saving output to the report
        self.log(Level.INFO, "Running program on data source")
        self.log(Level.INFO, self.path_to_exe + " -c match -i " + inputDir + " -d " + self.path_to_dirs + " -o " + reportPath)
        subprocess.Popen([self.path_to_exe, "-c", "match" , "-i", inputDir, "-d", self.path_to_dirs, "-o", reportPath], stdout=logHandle).communicate()   
        logHandle.close()
        
        # Add the report to the case, so it shows up in the tree
        Case.getCurrentCase().addReport(reportPath, "Run EXE", "ATPR result output")
        
        self.log(Level.INFO, "Report created on "+ reportPath)
        
        self.log(Level.INFO, "Adding elements to Autopsy")

        #Open with codecs for utf-8 parsing
        result = codecs.open(reportPath, encoding='utf-8')

        files_array = set()
        for line in result:
            fields = line.split(';')
            fields_splitted = fields[0].split("\\")
            files_array.add( fields_splitted[len(fields_splitted) - 1] )

        fileManager = Case.getCurrentCase().getServices().getFileManager()

        result.close()

        #Adding elements to autopsy reports interface
        for item in files_array:

            files = fileManager.findFiles(dataSource, item)  
            
            for file in files:

                result = codecs.open(reportPath, encoding='utf-8')

                for line in result:
                    fields = line.split(';')

                    if file.getName() in fields[0]:
                        art = file.newArtifact(artID_eu)
                        
                        art.addAttributes(((BlackboardAttribute(attID_fp, RunExeIngestModuleFactory.moduleName, fields[0])), \
                        (BlackboardAttribute(attID_match, RunExeIngestModuleFactory.moduleName, fields[1])), \
                        (BlackboardAttribute(attTotal_match, RunExeIngestModuleFactory.moduleName, fields[2])), \
                        (BlackboardAttribute(attType_match, RunExeIngestModuleFactory.moduleName, fields[3])), \
                        (BlackboardAttribute(attDict_match, RunExeIngestModuleFactory.moduleName, fields[4]))))

                IngestServices.getInstance().fireModuleDataEvent(ModuleDataEvent(RunExeIngestModuleFactory.moduleName, artID_eu_evt, None))
        
                result.close()

        self.log(Level.INFO, "Done")

        return IngestModule.ProcessResult.OK