using System.Reflection;
using System.Windows.Forms;

namespace XCOM2Launcher.PropertyGrid
{
    static class PropertyGridExtensions
    {
        /// <summary>
        /// Resize the columns of a PropertyGrid.  From http://stackoverflow.com/questions/12447156/14475276#14475276
        /// </summary>
        public static void SetLabelColumnWidth(this System.Windows.Forms.PropertyGrid grid, int width)
        {
            FieldInfo fi = grid?.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic);
            Control view = fi?.GetValue(grid) as Control;
            MethodInfo mi = view?.GetType().GetMethod("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic);

            mi?.Invoke(view, new object[] { width });
            view?.Invalidate();
        }
    }
}
