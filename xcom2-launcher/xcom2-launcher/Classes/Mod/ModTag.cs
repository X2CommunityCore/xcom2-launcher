using System;
using System.Drawing;

namespace XCOM2Launcher.Mod
{
    public class ModTag
    {
        public string Label { get; set; } = "New Tag";

        public Color Color { get; set; } = RandomColor();

        public ModTag() { }

        public ModTag(string label, Color? color = null)
        {
            Color = color ?? RandomColor();
            Label = label;
        }

        public static Color RandomColor()
        {
            var randomGen = new Random();
            var names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            var randomColorName = names[randomGen.Next(names.Length)];

            return Color.FromKnownColor(randomColorName);
        }
    }
}
