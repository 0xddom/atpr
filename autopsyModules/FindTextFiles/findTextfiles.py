# Looks for text files that contain extension doc, docx, txt and pdf


import jarray
import inspect
import os
from java.lang import System
from java.io import File
from java.util.logging import Level
from org.sleuthkit.datamodel import SleuthkitCase
from org.sleuthkit.datamodel import AbstractFile
from org.sleuthkit.datamodel import ReadContentInputStream
from org.sleuthkit.datamodel import BlackboardArtifact
from org.sleuthkit.datamodel import BlackboardAttribute
from org.sleuthkit.datamodel import TskData
from org.sleuthkit.autopsy.ingest import IngestModule
from org.sleuthkit.autopsy.ingest.IngestModule import IngestModuleException
from org.sleuthkit.autopsy.ingest import DataSourceIngestModule
from org.sleuthkit.autopsy.ingest import FileIngestModule
from org.sleuthkit.autopsy.ingest import IngestModuleFactoryAdapter
from org.sleuthkit.autopsy.ingest import IngestMessage
from org.sleuthkit.autopsy.ingest import IngestServices
from org.sleuthkit.autopsy.ingest import ModuleDataEvent
from org.sleuthkit.autopsy.coreutils import Logger
from org.sleuthkit.autopsy.casemodule import Case
from org.sleuthkit.autopsy.casemodule.services import Services
from org.sleuthkit.autopsy.casemodule.services import FileManager
from org.sleuthkit.autopsy.datamodel import ContentUtils
# This will work in 4.0.1 and beyond
# from org.sleuthkit.autopsy.casemodule.services import Blackboard

# Factory that defines the name and details of the module and allows Autopsy
# to create instances of the modules that will do the anlaysis.
class FindTextFilesModuleFactory(IngestModuleFactoryAdapter):

    moduleName = "Text Files Finder"

    def getModuleDisplayName(self):
        return self.moduleName

    def getModuleDescription(self):
        return "Module that find text files (txt,pdf, doc and docx)."

    def getModuleVersionNumber(self):
        return "1.0"

    # Return true if module wants to get called for each file
    def isFileIngestModuleFactory(self):
        return True

    # can return null if isFileIngestModuleFactory returns false
    def createFileIngestModule(self, ingestOptions):
        return FindTextFilesIngestModule()


# File-level ingest module.  One gets created per thread.
class FindTextFilesIngestModule(FileIngestModule):

    _logger = Logger.getLogger(FindTextFilesModuleFactory.moduleName)

    def log(self, level, msg):
        self._logger.logp(level, self.__class__.__name__, inspect.stack()[1][3], msg)

    def startUp(self, context):
        self.filesFound = 0
        pass

    def process(self, file):

        # Skip non-files
        if ((file.getType() == TskData.TSK_DB_FILES_TYPE_ENUM.UNALLOC_BLOCKS) or 
            (file.getType() == TskData.TSK_DB_FILES_TYPE_ENUM.UNUSED_BLOCKS) or 
            (file.isFile() == False)):
            return IngestModule.ProcessResult.OK

        # Look for files with extension docx, ,doc, pdf and txt  
        extensions = ['docx', 'doc', 'pdf', 'txt']      

        if (file.getNameExtension() in extensions):

            # Make an artifact on the blackboard.  TSK_INTERESTING_FILE_HIT is a generic type of
            # artifact.  Refer to the developer docs for other examples.
            art = file.newArtifact(BlackboardArtifact.ARTIFACT_TYPE.TSK_INTERESTING_FILE_HIT)
            att = BlackboardAttribute(BlackboardAttribute.ATTRIBUTE_TYPE.TSK_SET_NAME.getTypeID(), 
                  FindTextFilesModuleFactory.moduleName, "Find text Files")


            # Create find text directory in module output directory, if it exists then continue on processing     
            outpuDir = Case.getCurrentCase().getModulesOutputDirAbsPath() + "\TextFiles"
            self.log(Level.INFO, "create Directory " + outpuDir)
            try:
                os.mkdir(outpuDir)
            except:
                self.log(Level.INFO, "Find Text Directory already exists " + outpuDir)

            configFilesPath = os.path.join(outpuDir, str(file.getName()))
            ContentUtils.writeToFile(file, File(configFilesPath))

            art.addAttribute(att)

            IngestServices.getInstance().fireModuleDataEvent(
                ModuleDataEvent(FindTextFilesModuleFactory.moduleName, 
                    BlackboardArtifact.ARTIFACT_TYPE.TSK_INTERESTING_FILE_HIT, None));

        return IngestModule.ProcessResult.OK
 

    def shutDown(self):
        None