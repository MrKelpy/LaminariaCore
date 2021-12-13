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
import LaminariaCore.PathUtils;
import LaminariaCore.IOUtils;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;


public class Main {

    public static void main(String[] args) {

        try { App.run(); }
        catch (Exception e) {

            LoggingUtils logger = new LoggingUtils();
            logger.LogFile(e.toString());
            logger.LogConsole(e.toString());

        }
    }
}
