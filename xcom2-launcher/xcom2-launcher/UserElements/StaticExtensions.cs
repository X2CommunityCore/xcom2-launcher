using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCOM2Launcher
{
   public static partial class StaticExtensions
    {

        // Source: http://stackoverflow.com/a/14475276/3951051
        public static void SetLabelColumnWidth(this System.Windows.Forms.PropertyGrid grid, int width)
        {
            if (grid == null)
                return;

            FieldInfo fi = grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi == null)
                return;

            Control view = fi.GetValue(grid) as Control;
            if (view == null)
                return;

            MethodInfo mi = view.GetType().GetMethod("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi == null)
                return;

            mi.Invoke(view, new object[] { width });

            view.Invalidate();
        }
    }
}
