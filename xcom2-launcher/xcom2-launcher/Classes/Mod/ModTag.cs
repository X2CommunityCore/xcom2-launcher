using System;
using System.Drawing;

namespace XCOM2Launcher.Mod
{
    public class ModTag
    {
        public string Label { get; set; } = "New Tag";

        public Color Color { get; set; } = Color.Black.GetRandom(0.75, 0.9).GetPastelShade();

        public ModTag() { }

        public ModTag(string label, Color? color = null)
        {
            Color = color ?? Color.Black.GetRandom(0.75, 0.9).GetPastelShade();
            Label = label;
        }
    }
}
