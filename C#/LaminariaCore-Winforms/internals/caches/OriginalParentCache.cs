using System.Collections.Generic;
using System.Windows.Forms;

namespace LaminariaCore_Winforms.internals.caches
{
    /// <summary>
    /// This singleton internal class implements a cache that stores every control's original
    /// parent, so that the dynamic loading/unloading of active components can be managed smoothly.
    /// </summary>
    internal class OriginalParentCache
    {
        /// <summary>
        /// The instance used to access the class, since it is a singleton.
        /// </summary>
        public static OriginalParentCache INSTANCE = new OriginalParentCache();

        /// <summary>
        /// The cache containing the original parents of every control.
        /// </summary>
        private Dictionary<string, Control> cache = new Dictionary<string, Control>();

        private OriginalParentCache()
        {
        }

        /// <summary>
        /// Adds the specified control to the cache, in the form of a name:parent mapping.
        /// </summary>
        /// <param name="control">The control to add to the cache.</param>
        /// <returns>Whether or not the control was successfully added to cache.</returns>
        internal bool AddToCache(Control control)
        {
            // There can only be one registry in the cache for each control, so if it already exists,
            // return false.
            if (Contains(control.Name)) return false;

            cache.Add(control.Name, control.Parent);
            return true;
        }

        /// <summary>
        /// Returns the parent of the specified control, as it was before it was added to the cache.
        /// </summary>
        /// <param name="name">The name of the control to look for in the cache</param>
        /// <returns>The cached original parent for the specified name</returns>
        internal Control GetParentOf(string name) => cache.ContainsKey(name) ? cache[name] : null;

        /// <summary>
        /// Checks if the specified control is cached.
        /// </summary>
        /// <param name="name">The control name to check for in the cache.</param>
        /// <returns>Whether the control exists in the cache or not.</returns>
        internal bool Contains(string name) => cache.ContainsKey(name);
    }
}