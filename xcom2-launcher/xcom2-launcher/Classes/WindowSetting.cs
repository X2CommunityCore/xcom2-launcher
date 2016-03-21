using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCOM2Launcher
{
    public class WindowSettings
    {
        public System.Drawing.Rectangle Bounds { get; set; }
        public FormWindowState State { get; set; }

        public byte[] Data { get; set; }
        public WindowSettings() { }
        public WindowSettings(Form form)
        {
            State = form.WindowState;

            // Restore state to get normal bounds
            if (form.WindowState != FormWindowState.Normal)
                form.WindowState = FormWindowState.Normal;

            Bounds = form.DesktopBounds;
        }
    }
}
