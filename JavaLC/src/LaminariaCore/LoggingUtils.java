/*
 * This class is distributed as part of the Laminaria Core (Java Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
package LaminariaCore;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

@SuppressWarnings("unused")
public class LoggingUtils {
    /* Class used to implement custom logging functions.
     */

    private final String path;  // Logs folder path
    private final String session;  // Current logger session

    public LoggingUtils(String path) {

        this.path = path;
        IOUtils.ensureFilepath(this.path);

        // Formatted datetime for the current logger session. Should be, for example, 2021-12-07.09.55.51
        LocalDateTime sessionDate = LocalDateTime.now();
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy-MM-dd.HH.mm.ss");
        this.session = sessionDate.format(dtf);
    }

    // Alternative constructor, assumes that the logs folder is in the cwd.
    public LoggingUtils() {

        this.path = PathUtils.join(System.getProperty("user.dir"), "logs");
        IOUtils.ensureFilepath(this.path);

        // Formatted datetime for the current logger session. Should be, for example, 2021-12-07.09.55.51
        LocalDateTime sessionDate = LocalDateTime.now();
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy-MM-dd.HH.mm.ss");
        this.session = sessionDate.format(dtf);
    }


    public String buildLogString(String message) {
        /* Builds a proper Logging String from a given raw message.
         * This method interacts with the Stack Trace in order to retrieve the caller namespace,
         * caller class and caller method, so as to provide more information at the time of Logging.
         */

        // Acquire the stacktrace useful information about the callers
        String fileName = Thread.currentThread().getStackTrace()[2].getFileName();
        String caller_class = Thread.currentThread().getStackTrace()[2].getClassName();
        String caller_method = Thread.currentThread().getStackTrace()[2].getMethodName();

        // Get the date of logging, formatted.
        LocalDateTime dateNow = LocalDateTime.now();
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");
        String dateFormatted = dateNow.format(dtf);

        return String.format("[%s -> %s.%s.%s] \"%s\"", dateFormatted, fileName, caller_class, caller_method, message);
    }


    public void logConsole(String message) {
        /* Sends a logging string to STDIN, taking into account the given
         * raw message and the caller attributes.
         */

        String loggingString = this.buildLogString(message);
        System.out.println(loggingString);
    }


    public void logFile(String message) {
        /* Writes a logging string to a file, taking into account the given
         * raw message, caller attributes and current logger session.
         */

        String loggingString = this.buildLogString(message);
        String filepath = PathUtils.join(this.path, this.session + ".log");
        IOUtils.ensureFilepath(filepath);
        IOUtils.appendFile(filepath, loggingString);
    }

}
