using System.IO;

namespace LaminariaCore_General.utils
{
    /// <summary>
    /// This class aims to provide a set of utilities to work with paths.
    /// </summary>
    public static class PathUtils
    {
        
        /// <summary>
        /// Normalizes a path, changing it to a common format.
        /// Every path should be formatted as such.: "path/to/directory"
        /// </summary>
        /// <param name="path">The path to normalize.</param>
        /// <returns>The normalized path.</returns>
        public static string NormalizePath(string path)
        {
            path = path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            path = path.Replace("\\", @"/");
            path = path.Replace(@"\", @"/");
            path = path.Replace(@"/./", @"/");
            return path;
        }
        
        /// <summary>
        /// Compares two paths, and returns true if they are equal.
        /// </summary>
        /// <param name="path">The target path, the one being called through the string</param>
        /// <param name="path2">The target to compare the first path with</param>
        /// <returns>Whether the paths are equal or not</returns>
        public static bool EqualsPath(this string path, string path2) => NormalizePath(path).Equals(NormalizePath(path2));
    }
}