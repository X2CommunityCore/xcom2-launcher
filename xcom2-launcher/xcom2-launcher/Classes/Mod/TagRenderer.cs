using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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
        
        public TagRenderInfo(Point offset, Rectangle bounds, Size textSize, Color tagColor)
        {
            var rectWidth = textSize.Width + tX * 2;
            var rectHeight = Math.Max(textSize.Height + tY * 2, bounds.Height - rY) - 1;
            var tagRectangle = new Rectangle(offset.X + rX, offset.Y + (bounds.Height - rectHeight) / 2 - 2, rectWidth, rectHeight);
            var shadowRectangle = new Rectangle(tagRectangle.X + 1, tagRectangle.Y + 1, tagRectangle.Width, tagRectangle.Height);

            TextPosition = new Point(tagRectangle.X + tX, offset.Y + tY - 1);
            ShadowPath = TagRenderer.RoundedRect(shadowRectangle, rX);
            Path = TagRenderer.RoundedRect(tagRectangle, rX);
            BorderColor = ControlPaint.Dark(tagColor);
            HitBox = tagRectangle;
            TagColor = tagColor;
        }

        public Point TextPosition { get; }
        public GraphicsPath Path { get; }
        public Rectangle HitBox { get; }
        public GraphicsPath ShadowPath { get; }
        public Color BorderColor { get; }
        public Color TagColor { get; }
    }

    public class TagRenderer : BaseRenderer
    {
        private readonly Dictionary<string, ModTag> _availableTags;
        
        
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

        public override bool RenderSubItem(DrawListViewSubItemEventArgs e, Graphics g, Rectangle r, object rowObject)
        {
            var mod = (ModEntry)rowObject;

            if (mod == null)
                return false;

            var listItem = ListView.Items[e.ItemIndex];
            var backgroundColor = rowObject == ListView.SelectedObject
                                ? ListItem?.SelectedBackColor ?? ListView.SelectedBackColorOrDefault
                                : listItem.BackColor;
            var tagList = _availableTags.Select(kvp => kvp.Value)
                                        .Where(t => mod.Tags.Contains(t.Label));

            return RenderTags(listItem.Font, backgroundColor, g, r, tagList.ToList());
        }

        public static bool RenderTags(Font font, Color backgroundColor, Graphics graphics, Rectangle bounds, IList<ModTag> tagList)
        {
            graphics.Clear(backgroundColor);

            if (tagList.Count <= 0)
                return true;

            var offset = new Point(bounds.X + TagRenderInfo.rX,
                                   bounds.Y + TagRenderInfo.rY);

            foreach (var tag in tagList)
            {
                var tagSize = graphics.MeasureString(tag.Label, font).ToSize();
                var renderInfo = new TagRenderInfo(offset, bounds, tagSize, tag.Color);

                using (var backgroundBrush = new SolidBrush(Color.FromArgb(255, 32, 32, 32)))
                {
                    graphics.FillPath(backgroundBrush, renderInfo.ShadowPath);
                }
                using (var backgroundBrush = new SolidBrush(renderInfo.TagColor))
                {
                    graphics.FillPath(backgroundBrush, renderInfo.Path);
                }
                using (var borderPen = new Pen(renderInfo.BorderColor))
                {
                    graphics.DrawPath(borderPen, renderInfo.Path);
                }
                using (var textBrush = new SolidBrush(renderInfo.BorderColor))
                {
                    graphics.DrawString(tag.Label, font, textBrush, renderInfo.TextPosition);
                }

                offset.X += renderInfo.HitBox.Width + TagRenderInfo.rX;
                // stop drawing outside of the column bounds
                if (offset.X > bounds.Right)
                    break;
            }

            return true;
        }
    }
}
