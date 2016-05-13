import os
import config

TOOLS_PATH = os.path.dirname(os.path.realpath(__file__))
ROOT_PATH = os.path.realpath(TOOLS_PATH + '/../')
UNITY_PROJECT_PATH = os.path.realpath(ROOT_PATH + '/' + config.UNITY_PROJECT_DIR +'/')
UNITY_LOG_FILE = os.path.realpath(ROOT_PATH + '/Gne-gne-log.txt')
