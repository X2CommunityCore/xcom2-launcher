namespace XCOM2Launcher.Mod
{
    public class ModClassOverride
    {
        public ModEntry Mod { get; private set; }
        public string NewClass { get; private set; }
        public string OldClass { get; private set; }
        public ModClassOverrideType OverrideType { get; private set; }

        // The original text line in unprocessed form, to avoid false positives (#102)
        public string TextLine { get; private set; }

        public ModClassOverride(ModEntry mod, string newClass, string oldClass, ModClassOverrideType overrideType, string textLine)
        {
            Mod = mod;
            NewClass = newClass;
            OldClass = oldClass;
            OverrideType = overrideType;
            TextLine = textLine;
        }
    }

    public enum ModClassOverrideType
    {
        Class,
        UIScreenListener
    }
}