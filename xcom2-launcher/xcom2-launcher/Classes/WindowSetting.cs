using System.Drawing;
using System.Windows.Forms;

namespace XCOM2Launcher
{
    public class WindowSettings
    {
        public WindowSettings()
        {
        }

        public WindowSettings(Form form)
        {
            State = form.WindowState;

            // Restore state to get normal bounds
            if (form.WindowState != FormWindowState.Normal)
                form.WindowState = FormWindowState.Normal;

            Bounds = form.DesktopBounds;
        }

        public Rectangle Bounds { get; set; }
        public FormWindowState State { get; set; }
        public byte[] Data { get; set; }
    }
}