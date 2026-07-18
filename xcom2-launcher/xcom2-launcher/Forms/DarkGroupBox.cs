using System.Drawing;
using System.Windows.Forms;

namespace XCOM2Launcher.Forms
{
    /// <summary>
    /// A GroupBox that can fully take over its own painting.
    ///
    /// The default GroupBox draws an etched border via the OS's themed renderer,
    /// which ignores BackColor/ForeColor and can't be recolored. Rather than trying
    /// to match it, this just doesn't draw a border at all - flat dark background
    /// plus the caption text, nothing else.
    /// </summary>
    public class DarkGroupBox : GroupBox
    {
        /// <summary>When false, this behaves like a completely normal GroupBox.</summary>
        public bool UseDarkTheme { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!UseDarkTheme)
            {
                base.OnPaint(e);
                return;
            }

            var g = e.Graphics;
            g.Clear(BackColor);

            using var textBrush = new SolidBrush(ForeColor);
            g.DrawString(Text, Font, textBrush, 6, 0);
        }
    }
}
