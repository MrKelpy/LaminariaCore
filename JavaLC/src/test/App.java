/*
 * This class is distributed as part of the Laminaria Core (Java Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
package test;

import LaminariaCore.LoggingUtils;

@SuppressWarnings("RedundantThrows")
public class App {

    public static void run() throws Exception {

        LoggingUtils logger = new LoggingUtils();
        logger.LogConsole("test");
        logger.LogFile("test");

    }

}
