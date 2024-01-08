using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using LaminariaCore_General.common.abstraction;
using LaminariaCore_General.utils;

namespace LaminariaCore_General.common
{
    /// <summary>
    /// This class implements an interface to interact with a program's file structure
    /// in a more convenient way.
    /// </summary>
    public class FileManager : AbstractBaseOperations
    {
        /// <summary>
        /// Main constructor for the FileManager class. Sets the root path to the specified value.
        /// If not specified, use the AppData/.PROGRAM-NAME folder.
        /// </summary>
        /// <param name="root">The path to the root directory of the file system.</param>
        /// <param name="appData">Whether or not to append the root to the appdata</param>
        public FileManager(string root = null, bool appData = false) : base(root)
        {
            // If the root isn't specified, use the AppData/.PROGRAM-NAME folder.
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            if (root != null && appData)
                root = Path.Combine(appDataPath, root);
            
            RootPath = OperationsTargetPath = root ?? Path.Combine(appDataPath, $".{Assembly.GetCallingAssembly().GetName().Name}");
            RootPath = OperationsTargetPath = PathUtils.NormalizePath(RootPath);
            
            FileUtils.EnsurePath(RootPath, FileAttributes.Directory);
        }
        
        /// <summary>
        /// Blocks the thread waiting for a file to be released by another process and returns
        /// it as a FileStream when it eventually is.
        /// </summary>
        /// <param name="path">The path of the file to get</param>
        /// <param name="timeout">A maximum timeout in milliseconds</param>
        public static async Task<FileStream> WaitForFileAsync(string path, int timeout = 10000)
        {
            DateTime maxTimeout = DateTime.Now.AddMilliseconds(timeout);

            while (DateTime.Now <= maxTimeout)
            {
                try
                {
                    // Tries to open the file, if it fails, continue the loop until the timeout is reached.
                    Stream stream = new FileStream(path, FileMode.Open);
                    return (FileStream)stream;
                }
                catch (IOException) { await Task.Delay(100); }
            }

            throw new TimeoutException("The file could not be accessed within the specified timeout.");
        }
    }
}