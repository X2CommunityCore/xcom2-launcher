using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Microsoft.Win32;

namespace XCOM2Launcher.Forms
{
    /// <summary>
    /// Applies a dark color scheme to a WinForms form and every control inside it,
    /// including the native Windows 10/11 dark title bar. WinForms has no built-in
    /// dark mode, so this walks the control tree and special-cases the few control
    /// types (ToolStrip/MenuStrip, ObjectListView, DataGridView, PropertyGrid,
    /// TabControl, GroupBox) that ignore plain BackColor/ForeColor changes.
    ///
    /// Usage:
    ///   ThemeManager.Apply(this, Settings.DarkMode);   // call once after the form's handle exists
    /// </summary>
    public static class ThemeManager
    {
        // ---- Palette ---------------------------------------------------------
        // Loosely based on the VS Code / modern WinForms dark theme, with an
        // amber accent instead of blue to match the XCOM UI a bit better.
        public static readonly Color WindowBackground = Color.FromArgb(32, 32, 32);
        public static readonly Color ControlBackground = Color.FromArgb(45, 45, 48);
        public static readonly Color ControlBackgroundAlt = Color.FromArgb(37, 37, 38);
        public static readonly Color Border = Color.FromArgb(63, 63, 70);
        public static readonly Color Text = Color.FromArgb(241, 241, 241);
        public static readonly Color Accent = Color.FromArgb(224, 138, 60);

        // ---- Public API --------------------------------------------------------

        /// <summary>Applies (or reverts) the dark theme to a form and all its children.</summary>
        public static void Apply(Form form, bool dark)
        {
            ApplyDarkTitleBar(form, dark);
            ApplyToControl(form, dark);
            form.Refresh();
        }

        /// <summary>True if Windows itself is currently set to dark app mode. Handy as a default for first run.</summary>
        public static bool IsSystemDarkModeEnabled()
        {
            const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(keyPath))
                {
                    var value = key?.GetValue("AppsUseLightTheme");
                    return value is int i && i == 0;
                }
            }
            catch
            {
                return false; // registry value missing (older Windows) -> default to light
            }
        }

        // ---- Title bar (Windows 10 1809+ / 11) ---------------------------------

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int valueSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20; // Windows 10 20H1+ and 11
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_OLD = 19; // Windows 10 1809-1909

        private static void ApplyDarkTitleBar(Form form, bool dark)
        {
            if (!form.IsHandleCreated)
                return;

            int useDark = dark ? 1 : 0;
            // Try the modern attribute first (Windows 10 20H1+),
            // fall back to the older one (1809-1909).
            // If neither is supported the calls return non-zero and we just no-op.
            if (DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int)) != 0)
                DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE_OLD, ref useDark, sizeof(int));
        }

        // ---- Scrollbars & control borders ---------------------------------------
        // BackColor/ForeColor never touch these - native list/scroll controls are
        // painted by Windows' own Common Controls theme engine, not by .NET. The fix
        // is to switch each control to the same hidden "DarkMode_Explorer" theme
        // class that Explorer and Notepad use for their own dark scrollbars.

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        // These two are undocumented (ordinal-only) exports, stable since Windows 10
        // 1809 and used internally by Explorer/Notepad/Windows Terminal for the same
        // purpose. Wrapped in try/catch below so older Windows builds just no-op.
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        private static extern int SetPreferredAppMode(int preferredAppMode);

        [DllImport("uxtheme.dll", EntryPoint = "#136")]
        private static extern void FlushMenuThemes();

        /// <summary>Call once at startup, before the first window is shown (e.g. in Program.Main).</summary>
        public static void EnableAppDarkModeSupport()
        {
            try
            {
                SetPreferredAppMode(1); // 1 = AllowDark
                FlushMenuThemes();
            }
            catch
            {
                // Pre-1809 Windows - dark scrollbars/borders just won't be available there.
            }
        }

        private static void SetDarkScrollBars(Control control, bool dark)
        {
            if (!control.IsHandleCreated)
                return;

            try
            {
                SetWindowTheme(control.Handle, dark ? "DarkMode_Explorer" : "Explorer", null);
            }
            catch
            {
                // Same fallback as above - just leave the control's native styling alone.
            }
        }

        // ---- Control tree walker -----------------------------------------------

        private static void ApplyToControl(Control control, bool dark)
        {
            SetDarkScrollBars(control, dark);

            switch (control)
            {
                case Form f:
                    f.BackColor = dark ? WindowBackground : SystemColors.Control;
                    f.ForeColor = dark ? Text : SystemColors.ControlText;
                    break;

                case MenuStrip menu:
                    menu.Renderer = dark ? new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    menu.BackColor = dark ? ControlBackground : SystemColors.Control;
                    menu.ForeColor = dark ? Text : SystemColors.ControlText;
                    foreach (ToolStripItem item in menu.Items)
                        ApplyToToolStripItem(item, dark);
                    break;

                case StatusStrip status:
                    status.Renderer = dark ? new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    status.BackColor = dark ? ControlBackground : SystemColors.Control;
                    status.ForeColor = dark ? Text : SystemColors.ControlText;
                    foreach (ToolStripItem item in status.Items)
                        ApplyToToolStripItem(item, dark);
                    break;

                case ToolStrip strip:
                    strip.Renderer = dark ? new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    strip.BackColor = dark ? ControlBackground : SystemColors.Control;
                    strip.ForeColor = dark ? Text : SystemColors.ControlText;
                    foreach (ToolStripItem item in strip.Items)
                        ApplyToToolStripItem(item, dark);
                    break;

                case ObjectListView olv:
                    ApplyToObjectListView(olv, dark);
                    break;

                case DataGridView grid:
                    ApplyToDataGridView(grid, dark);
                    break;

                case PropertyGrid pg:
                    ApplyToPropertyGrid(pg, dark);
                    break;

                case TextBoxBase tb: // covers TextBox and RichTextBox
                    tb.BackColor = dark ? ControlBackgroundAlt : SystemColors.Window;
                    tb.ForeColor = dark ? Text : SystemColors.WindowText;
                    tb.BorderStyle = dark ? BorderStyle.None : BorderStyle.Fixed3D;
                    break;

                case ListBox lb:
                    lb.BackColor = dark ? ControlBackgroundAlt : SystemColors.Window;
                    lb.ForeColor = dark ? Text : SystemColors.WindowText;
                    lb.BorderStyle = dark ? BorderStyle.None : BorderStyle.Fixed3D;
                    break;

                case ComboBox cb:
                    cb.BackColor = dark ? ControlBackgroundAlt : SystemColors.Window;
                    cb.ForeColor = dark ? Text : SystemColors.WindowText;
                    // Flat style is required in dark mode - the default Win32 renderer
                    // draws its own light border/button that ignores BackColor entirely.
                    cb.FlatStyle = dark ? FlatStyle.Flat : FlatStyle.Standard;
                    break;

                case Button btn:
                    btn.BackColor = dark ? ControlBackground : SystemColors.Control;
                    btn.ForeColor = dark ? Text : SystemColors.ControlText;
                    btn.FlatStyle = dark ? FlatStyle.Flat : FlatStyle.Standard;
                    btn.FlatAppearance.BorderColor = dark ? Border : SystemColors.ControlDark;
                    break;

                case TabControl tabs:
                    ApplyToTabControl(tabs, dark);
                    break;

                case SplitContainer split:
                    split.BackColor = dark ? Border : SystemColors.Control;
                    break;

                case GroupBox box:
                    ApplyToGroupBox(box, dark);
                    break;

                default:
                    control.BackColor = dark ? WindowBackground : SystemColors.Control;
                    control.ForeColor = dark ? Text : SystemColors.ControlText;
                    break;
            }

            foreach (Control child in control.Controls)
                ApplyToControl(child, dark);

            // ContextMenuStrip is a floating component, not a child control, so the
            // recursive walk above never reaches it. Check each control explicitly.
            if (control.ContextMenuStrip != null)
                ApplyToContextMenuStrip(control.ContextMenuStrip, dark);
        }

        public static void ApplyToContextMenuStrip(ContextMenuStrip cms, bool dark)
        {
            cms.Renderer = dark ? new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
            cms.BackColor = dark ? ControlBackground : SystemColors.Control;
            cms.ForeColor = dark ? Text : SystemColors.ControlText;
            foreach (ToolStripItem item in cms.Items)
                ApplyToToolStripItem(item, dark);
        }

        private static void ApplyToToolStripItem(ToolStripItem item, bool dark)
        {
            item.BackColor = dark ? ControlBackground : SystemColors.Control;
            item.ForeColor = dark ? Text : SystemColors.ControlText;

            if (item is ToolStripMenuItem menuItem)
                foreach (ToolStripItem sub in menuItem.DropDownItems)
                    ApplyToToolStripItem(sub, dark);
        }

        // ---- ObjectListView (BrightIdeasSoftware) -------------------------------
        // The column headers are drawn by the OS theme engine by default and will
        // ignore every color you set unless HeaderUsesThemes is turned off first.

        private static void ApplyToObjectListView(ObjectListView olv, bool dark)
        {
            olv.BackColor = dark ? ControlBackgroundAlt : SystemColors.Window;
            olv.ForeColor = dark ? Text : SystemColors.WindowText;

            olv.UseAlternatingBackColors = true;
            olv.AlternateRowBackColor = dark ? ControlBackground : Color.FromArgb(247, 247, 247);

            olv.UnfocusedSelectedBackColor = dark ? Border : SystemColors.Control;
            olv.UnfocusedSelectedForeColor = dark ? Text : SystemColors.ControlText;

            olv.HeaderUsesThemes = !dark;
            olv.HeaderFormatStyle = dark ? BuildDarkHeaderStyle() : null;

            olv.BorderStyle = dark ? BorderStyle.None : BorderStyle.Fixed3D;
        }

        private static HeaderFormatStyle BuildDarkHeaderStyle()
        {
            var style = new HeaderFormatStyle();
            style.Normal.BackColor = ControlBackground;
            style.Normal.ForeColor = Text;
            style.Hot.BackColor = Border;
            style.Hot.ForeColor = Text;
            style.Pressed.BackColor = Accent;
            style.Pressed.ForeColor = Color.Black;
            return style;
        }

        // ---- DataGridView ---------------------------------------------------------

        private static void ApplyToDataGridView(DataGridView grid, bool dark)
        {
            grid.BackgroundColor = dark ? ControlBackgroundAlt : SystemColors.Window;
            grid.GridColor = dark ? Border : SystemColors.ControlDark;
            grid.EnableHeadersVisualStyles = !dark;
            grid.BorderStyle = dark ? BorderStyle.None : BorderStyle.Fixed3D;

            grid.DefaultCellStyle.BackColor = dark ? ControlBackgroundAlt : SystemColors.Window;
            grid.DefaultCellStyle.ForeColor = dark ? Text : SystemColors.WindowText;
            grid.DefaultCellStyle.SelectionBackColor = dark ? Accent : SystemColors.Highlight;
            grid.DefaultCellStyle.SelectionForeColor = dark ? Color.Black : SystemColors.HighlightText;

            grid.ColumnHeadersDefaultCellStyle.BackColor = dark ? ControlBackground : SystemColors.Control;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = dark ? Text : SystemColors.ControlText;

            grid.RowHeadersDefaultCellStyle.BackColor = dark ? ControlBackground : SystemColors.Control;
            grid.RowHeadersDefaultCellStyle.ForeColor = dark ? Text : SystemColors.ControlText;
        }

        // ---- PropertyGrid -----------------------------------------------------------
        // PropertyGrid exposes its own dedicated color properties instead of using
        // BackColor/ForeColor for most of its surface, so each one has to be set.

        private static void ApplyToPropertyGrid(PropertyGrid grid, bool dark)
        {
            grid.BackColor = dark ? ControlBackground : SystemColors.Control;
            grid.LineColor = dark ? Border : SystemColors.ControlDark;

            grid.ViewBackColor = dark ? ControlBackgroundAlt : SystemColors.Window;
            grid.ViewForeColor = dark ? Text : SystemColors.WindowText;

            grid.CategoryForeColor = dark ? Text : SystemColors.ControlText;
            grid.CategorySplitterColor = dark ? Border : SystemColors.Control;

            grid.HelpBackColor = dark ? ControlBackground : SystemColors.Control;
            grid.HelpForeColor = dark ? Text : SystemColors.ControlText;

            grid.CommandsBackColor = dark ? ControlBackground : SystemColors.Control;
            grid.CommandsForeColor = dark ? Text : SystemColors.ControlText;
        }

        // ---- TabControl ---------------------------------------------------------------
        // Like GroupBox, the tab headers are drawn by the OS theme engine and ignore
        // BackColor/ForeColor completely. Unlike GroupBox, TabControl has a proper
        // built-in owner-draw mode for this - no subclassing needed.

        private static void ApplyToTabControl(TabControl tabs, bool dark)
        {
            tabs.BackColor = dark ? WindowBackground : SystemColors.Control;
            tabs.ForeColor = dark ? Text : SystemColors.ControlText;

            foreach (TabPage page in tabs.TabPages)
            {
                page.BackColor = dark ? WindowBackground : SystemColors.Control;
                page.ForeColor = dark ? Text : SystemColors.ControlText;
            }

            if (tabs is DarkTabControl dtc)
            {
                // DarkTabControl's OnPaint override handles everything - both the tab
                // header buttons and the page-content frame - in one pass. Setting
                // DrawMode here would conflict with that and break the painting.
                dtc.UseDarkTheme = dark;
            }
            else
            {
                // Best-effort fallback for any plain TabControl not converted to
                // DarkTabControl. OwnerDrawFixed covers the tab header buttons only;
                // the page-content frame stays OS-themed.
                tabs.DrawMode = dark ? TabDrawMode.OwnerDrawFixed : TabDrawMode.Normal;
                tabs.DrawItem -= DrawDarkTabItem;
                if (dark)
                    tabs.DrawItem += DrawDarkTabItem;
            }

            tabs.Invalidate();
        }

        private static void DrawDarkTabItem(object sender, DrawItemEventArgs e)
        {
            var tabs = (TabControl)sender;
            var page = tabs.TabPages[e.Index];
            bool selected = e.Index == tabs.SelectedIndex;

            using (var bg = new SolidBrush(selected ? ControlBackground : WindowBackground))
                e.Graphics.FillRectangle(bg, e.Bounds);

            var textColor = selected ? Accent : Text;
            TextRenderer.DrawText(e.Graphics, page.Text, tabs.Font, e.Bounds, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // ---- GroupBox ---------------------------------------------------------------
        // GroupBox is the one common control with no border-color property whatsoever -
        // the etched bevel is baked into its OnPaint with no way to override it through
        // properties. The only fix is to paint our own border over the top of it.

        private static void ApplyToGroupBox(GroupBox box, bool dark)
        {
            box.BackColor = dark ? WindowBackground : SystemColors.Control;
            box.ForeColor = dark ? Text : SystemColors.ControlText;

            if (box is DarkGroupBox dgb)
            {
                // DarkGroupBox owns its own painting entirely - nothing more to do.
                dgb.UseDarkTheme = dark;
            }
            else
            {
                // Best-effort fallback for any GroupBox not converted to DarkGroupBox.
                // The OS-themed outline mostly ignores this, but it's better than nothing.
                box.Paint -= PaintDarkGroupBoxBorder;
                if (dark)
                    box.Paint += PaintDarkGroupBoxBorder;
            }

            box.Invalidate();
        }

        private static void PaintDarkGroupBoxBorder(object sender, PaintEventArgs e)
        {
            var box = (GroupBox)sender;
            var g = e.Graphics;
            var textSize = g.MeasureString(box.Text, box.Font);

            int top = box.ClientRectangle.Top + (int)(textSize.Height / 2);
            var rect = Rectangle.FromLTRB(box.ClientRectangle.Left, top,
                box.ClientRectangle.Right - 1, box.ClientRectangle.Bottom - 1);

            // Blank out the strip across the top where the default caption/border sat.
            using (var bg = new SolidBrush(box.BackColor))
                g.FillRectangle(bg, 0, 0, box.Width, (int)textSize.Height + 2);

            using (var pen = new Pen(Border))
            {
                g.DrawLine(pen, rect.Left, rect.Top, rect.Left + 6, rect.Top);
                g.DrawLine(pen, rect.Left + 8 + (int)textSize.Width, rect.Top, rect.Right, rect.Top);
                g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom);
                g.DrawLine(pen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);
                g.DrawLine(pen, rect.Right, rect.Top, rect.Right, rect.Bottom);
            }

            using (var textBrush = new SolidBrush(Text))
                g.DrawString(box.Text, box.Font, textBrush, rect.Left + 6, box.ClientRectangle.Top);
        }
    }

    /// <summary>
    /// Custom renderer for MenuStrip/ToolStrip/StatusStrip. These three controls
    /// draw their backgrounds, borders and selection highlight via a ProfessionalColorTable
    /// rather than BackColor, so a normal recursive color pass has no effect on them.
    /// </summary>
    internal class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer() : base(new DarkColorTable()) { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = ThemeManager.Text;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = ThemeManager.Text;
            base.OnRenderArrow(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // Skip the base call: it draws a light 3D border that shows up as a
            // bright line around dark menus/toolbars.
        }
    }

    internal class DarkColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => ThemeManager.ControlBackground;
        public override Color ImageMarginGradientBegin => ThemeManager.ControlBackground;
        public override Color ImageMarginGradientMiddle => ThemeManager.ControlBackground;
        public override Color ImageMarginGradientEnd => ThemeManager.ControlBackground;

        public override Color MenuStripGradientBegin => ThemeManager.ControlBackground;
        public override Color MenuStripGradientEnd => ThemeManager.ControlBackground;

        public override Color MenuItemSelected => ThemeManager.Border;
        public override Color MenuItemSelectedGradientBegin => ThemeManager.Border;
        public override Color MenuItemSelectedGradientEnd => ThemeManager.Border;
        public override Color MenuItemPressedGradientBegin => ThemeManager.Accent;
        public override Color MenuItemPressedGradientEnd => ThemeManager.Accent;

        public override Color MenuBorder => ThemeManager.Border;
        public override Color MenuItemBorder => ThemeManager.Border;
        public override Color ToolStripBorder => ThemeManager.Border;

        public override Color SeparatorDark => ThemeManager.Border;
        public override Color SeparatorLight => ThemeManager.Border;

        public override Color StatusStripGradientBegin => ThemeManager.ControlBackground;
        public override Color StatusStripGradientEnd => ThemeManager.ControlBackground;

        public override Color ButtonSelectedHighlight => ThemeManager.Border;
        public override Color ButtonSelectedGradientBegin => ThemeManager.Border;
        public override Color ButtonSelectedGradientEnd => ThemeManager.Border;
        public override Color ButtonPressedGradientBegin => ThemeManager.Accent;
        public override Color ButtonPressedGradientEnd => ThemeManager.Accent;
    }
}
