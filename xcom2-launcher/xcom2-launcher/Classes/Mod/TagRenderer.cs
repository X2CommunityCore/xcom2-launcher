using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace XCOM2Launcher.Mod
{
    internal class TagRenderInfo
    {
        private static Size rectPadding = new Size(3, 1);
        private static Size textPadding = new Size(4, 1);

        public static int rX => rectPadding.Width;
        public static int rY => rectPadding.Height;
        public static int tX => textPadding.Width;
        public static int tY => textPadding.Height;

        public static Point offset;

        public TagRenderInfo(Rectangle bounds, Size textSize, Color tagColor)
        {
            var rectWidth = textSize.Width + tX * 2;
            var rectHeight = Math.Max(textSize.Height + tY * 2, bounds.Height - rY);
            var tagRectangle = new Rectangle(offset.X + rX, offset.Y + (bounds.Height - rectHeight) / 2 - 1, rectWidth, rectHeight);

            Path = TagRenderer.RoundedRect(tagRectangle, rX);
            BorderColor = ControlPaint.Dark(tagColor);
            TextPosition = new Point(tagRectangle.X + tX, offset.Y + tY);
            TextColor = tagColor;

            offset.X += rectWidth + rectPadding.Width;
        }

        public Point TextPosition { get; }
        public GraphicsPath Path { get; }
        public Color BorderColor { get; }
        public Color TextColor { get; }
    }

    public class TagRenderer : BaseRenderer
    {
        private readonly Dictionary<string, ModTag> _availableTags;
        private readonly Dictionary<int, TagRenderInfo> _tagRenderInfo = new Dictionary<int, TagRenderInfo>();
        
        public TagRenderer(ObjectListView listView, Dictionary<string, ModTag> availableTags)
        {
            _availableTags = availableTags;
            ListView = listView;
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var diameter = radius * 2;
            var path = new GraphicsPath();
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        /*
                public override void HitTest(OlvListViewHitTestInfo hti, int x, int y)
                {

                }
        */

        public override bool RenderSubItem(DrawListViewSubItemEventArgs e, Graphics g, Rectangle r, object rowObject)
        {
            var mod = (ModEntry)rowObject;

            if (mod == null)
                return false;

            g.Clear(rowObject == ListView.SelectedObject 
                  ? ListItem?.SelectedBackColor ?? ListView.SelectedBackColorOrDefault 
                  : ListView.Items[e.ItemIndex].BackColor);

            if (mod.Tags.Count <= 0)
                return true;

            TagRenderInfo.offset = new Point(r.X + TagRenderInfo.rX, 
                                             r.Y + TagRenderInfo.rY);

            foreach (var tagName in mod.Tags)
            {
                TagRenderInfo renderInfo;
                var tag = _availableTags[tagName];
                var tagKey = (mod.GetHashCode() * 397) ^ tag.GetHashCode();
                
                if (_tagRenderInfo.ContainsKey(tagKey) == false)
                {
                    var tagSize = g.MeasureString(tag.Label, GetFont()).ToSize();

                    _tagRenderInfo[tagKey] = renderInfo = new TagRenderInfo(r, tagSize, tag.Color);
                }
                else
                {
                    renderInfo = _tagRenderInfo[tagKey];
                }

                using (var backgroundBrush = new SolidBrush(tag.Color))
                {
                    g.FillPath(backgroundBrush, renderInfo.Path);
                }
                using (var borderPen = new Pen(renderInfo.BorderColor))
                {
                    g.DrawPath(borderPen, renderInfo.Path);
                }
                using (var textBrush = new SolidBrush(renderInfo.BorderColor))
                {
                    g.DrawString(tag.Label, GetFont(), textBrush, renderInfo.TextPosition);
                }
                // stop drawing outside of the column bounds
                if (TagRenderInfo.offset.X > r.Right)
                    break;
            }

            return true;
        }

        protected Font GetFont()
        {
            return Font ?? ListView.Font;
        }
    }
}
