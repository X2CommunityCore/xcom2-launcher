using System;
using System.Drawing;

namespace XCOM2Launcher.Mod
{
    public class ModTag
    {
        public string Label { get; set; } = "New Tag";

        public Color Color { get; set; } = RandomColor();

        public static Color RandomColor()
        {
            var newColor = Color.Black.GetRandom(0.75, 0.9);
            var random = new Random(newColor.B);

            return random.NextDouble() <= 0.5 ? newColor.GetPastelShade() : newColor;
        }

        public ModTag() { }

        public ModTag(string label, Color? color = null)
        {
            Color = color ?? RandomColor();
            Label = label;
        }
    }
}
