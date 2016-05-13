import os
import constants
import config

BUILD_COMMAND_LINE = '%s -projectPath %s -logFile %s -nographics -batchmode -quit -executeMethod Builder.AllCLI' % (
    config.UNITY_BIN_PATH,
    constants.UNITY_PROJECT_PATH,
    constants.UNITY_LOG_FILE
    )

print 'START EXPORT'
ret = os.system(BUILD_COMMAND_LINE)
if (ret != 0) :
    print 'EXPORT ERROR (%d)' % ret
else :
    print 'EXPORT SUCCESS'
