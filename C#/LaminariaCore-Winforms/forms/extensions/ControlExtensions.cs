using System.Windows.Forms;
using LaminariaCore_Winforms.internals.caches;

// ReSharper disable InconsistentNaming

namespace LaminariaCore_Winforms.forms.extensions
{
    /// <summary>
    /// This class is used to extend the functionality of every control in Windows Forms.
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Searches for the original parent of the control inside OriginalParentCache, and sets the control's
        /// parent back to it.
        /// </summary>
        /// <param name="self">The control the method is being called from</param>
        public static void SendToOriginal(this Control self)
        {
            Control parent = OriginalParentCache.INSTANCE.GetParentOf(self.Name);
            if (parent == null) return;
            parent.Controls.Add(self);
        }

        /// <summary>
        /// Checks whether the control is already contained in the cache.
        /// </summary>
        /// <param name="self">The control the method is being called from</param>
        /// <returns>Whether the control is cached or not.</returns>
        public static bool IsCached(this Control self) => OriginalParentCache.INSTANCE.Contains(self.Name);

        /// <summary>
        /// Force the caching of the original parent of the control.
        /// <br></br>
        /// You're most likely doing something wrong if you're using this method. It shouldn't be
        /// used by you in 99% of the cases, but it's here for that 1%.
        /// If you're using this method, make sure you know what you're doing.
        /// </summary>
        /// <param name="self">The control the method is being called from</param>
        public static void ForceCache(this Control self)
        {
            if (self.IsCached()) return;
            OriginalParentCache.INSTANCE.AddToCache(self);
        }
    }
}