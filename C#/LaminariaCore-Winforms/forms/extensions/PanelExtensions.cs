using System.Linq;
using System.Windows.Forms;

namespace LaminariaCore_Winforms.forms.extensions
{
    /// <summary>
    /// This class implements methods to extend the functionality of Panels in Windows Forms.
    /// </summary>
    public static class PanelExtensions
    {
        /// <summary>
        /// This extension method is used to add the contents from an external panel into
        /// the current panel.
        /// </summary>
        /// <param name="self">The panel the method is being called from</param>
        /// <param name="targetPanel">The panel to add the contents of</param>
        public static void AddAllFrom(this Panel self, Panel targetPanel)
        {
            // Loop through the controls in the target panel and add them to the current panel
            foreach (Control control in targetPanel.Controls.OfType<Control>().ToList())
            {
                control.ForceCache();
                self.Controls.Add(control);
            }
        }

        /// <summary>
        /// Replaces all the controls from the current panel with the controls from the target panel.
        /// </summary>
        /// <param name="self">The panel the method is being called from</param>
        /// <param name="targetPanel">The panel to clone the contents of</param>
        public static void SetAllFrom(this Panel self, Panel targetPanel)
        {
            self.Controls.OfType<Control>().ToList().ForEach(x => x.SendToOriginal());
            self.AddAllFrom(targetPanel);
        }
    }
}