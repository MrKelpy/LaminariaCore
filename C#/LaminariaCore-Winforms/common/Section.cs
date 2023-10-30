using System.IO;
using PgpsUtilsAEFC.common.abstraction;
using PgpsUtilsAEFC.utils;

namespace PgpsUtilsAEFC.common
{
    /// <summary>
    /// This class represents a Section inside the File System provided by the FileManager class. It can't
    /// be instantiated by the user, and will only be accessible through the FileManager.
    /// </summary>
    public class Section : AbstractBaseOperations
    {
        /// <summary>
        /// The full path for the section.
        /// </summary>
        public string SectionFullPath { get; set; }

        /// <summary>
        /// The section name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The simplified name of the section. This is the name without the relative path.
        /// </summary>
        public string SimpleName => Path.GetFileName(this.Name);

        /// <summary>
        /// Main constructor for the Section class. Takes in the root of the file system and the full
        /// path of the section.
        /// </summary>
        /// <param name="sectionPath">The full path of the section.</param>
        /// <param name="root">The full root path of the filesystem</param>
        internal Section(string sectionPath, string root) : base(sectionPath)
        {
            this.SectionFullPath = PathUtils.NormalizePath(sectionPath);
            this.Name = PathUtils.NormalizePath(this.SectionFullPath.Substring(root.Length));
            this.RootPath = root;
        }
    }
}