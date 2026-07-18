using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XCOM2Launcher.Forms
{
    /// <summary>
    /// A TabControl that fully takes over its own Win32 rendering.
    ///
    /// Overriding OnPaint alone isn't enough - the native WC_TABCONTROL draws its
    /// border frame via WM_PAINT before managed OnPaint even runs, so the border
    /// reappears on top of anything we draw. We intercept WM_PAINT in WndProc and
    /// do all rendering ourselves, preventing the native control from drawing at all.
    /// </summary>
    public class DarkTabControl : TabControl
    {
        [DllImport("user32.dll")]
        private static extern bool ValidateRect(IntPtr hWnd, IntPtr lpRect);

        /// <summary>When false, this behaves like a completely normal TabControl.</summary>
        public bool UseDarkTheme { get; set; }

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;

            if (UseDarkTheme && m.Msg == WM_PAINT && TabCount > 0)
            {
                // Mark the window as valid so Windows doesn't keep requesting redraws,
                // then do all rendering ourselves via Graphics.FromHwnd.
                ValidateRect(Handle, IntPtr.Zero);
                using var g = Graphics.FromHwnd(Handle);
                Render(g);
                return; // Skip base.WndProc - the native border never gets drawn.
            }

            base.WndProc(ref m);
        }

        private void Render(Graphics g)
        {
            g.Clear(ThemeManager.WindowBackground);

            for (int i = 0; i < TabCount; i++)
            {
                var tabRect = GetTabRect(i);
                bool selected = i == SelectedIndex;

                // Slightly extend the selected tab downward so it visually connects
                // to the page area and doesn't look like a floating button.
                if (selected)
                    tabRect = new Rectangle(tabRect.X, tabRect.Y, tabRect.Width, tabRect.Height + 2);

                using (var bg = new SolidBrush(selected ? ThemeManager.ControlBackground : ThemeManager.WindowBackground))
                    g.FillRectangle(bg, tabRect);

                var textColor = selected ? ThemeManager.Accent : ThemeManager.Text;
                TextRenderer.DrawText(g, TabPages[i].Text, Font, tabRect, textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            // No border drawn around the page area - flat background only.
        }
    }
}
