using System;
using System.Diagnostics;
using System.IO;

namespace application {

    public class LoggingUtils {
        /* Built-In-Template Logging Class. 
         * This class implements logging in files and logging in the console.
         */

        private String path;  // Logs folder path.
        private String session;  // This logger's current session

        public LoggingUtils(String path) {
            this.path = path;
            this.session = DateTime.Now.ToString("yyyy-MM-dd.HH.mm.ss");
        }

        public LoggingUtils() {

            // If no path is specified, assume the path to be of the cwd.
            String cwd = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            this.path = Path.Join(cwd, "logs");
            this.session = DateTime.Now.ToString("yyyy-MM-dd.HH.mm.ss");
        }


        private String BuildLogString(String raw_message) {
            /* Builds a proper Logging String from a given raw message.
             * This method interacts with the Stack Trace in order to retrieve the caller namespace,
             * caller class and caller method, so as to provide more information at the time of Logging.
             */

            StackTrace stackTrace = new StackTrace();  // StackTrace to navigate to the required information.
            String caller_method = stackTrace.GetFrame(2).GetMethod().Name;
            var caller_class = stackTrace.GetFrame(2).GetMethod().ReflectedType;
            String namespace_ = caller_class.Namespace.ToString();
            String date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            return $"[{date} -> {namespace_}.{caller_class.Name}.{caller_method}] \"{raw_message}\"";
        }


        public void LogConsole(String message)
        {
            /* Sends a logging string to STDIN, taking into account the given
             * raw message and the caller attributes.
             */

            String loggingMsg = this.BuildLogString(message);
            Console.WriteLine(loggingMsg);
        }


        public void LogFile(String message)
        {
            /* Writes a logging string to a file, taking into account the given
             * raw message, caller attributes and current logger session.
             */

            String path = Path.Join(this.path, this.session + ".log");

            Directory.CreateDirectory(this.path);
            using (StreamWriter file = new StreamWriter(path, true))
            {
                String loggingMsg = this.BuildLogString(message);
                file.Write(loggingMsg);
            }
        }


    }
}
