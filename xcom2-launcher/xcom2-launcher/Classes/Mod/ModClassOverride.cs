namespace XCOM2Launcher.Mod
{
    public class ModClassOverride
    {
        public ModEntry Mod { get; private set; }
        public string NewClass { get; private set; }
        public string OldClass { get; private set; }
        public ModClassOverrideType OverrideType { get; private set; }

        public ModClassOverride(ModEntry mod, string newClass, string oldClass, ModClassOverrideType overrideType)
        {
            Mod = mod;
            NewClass = newClass;
            OldClass = oldClass;
            OverrideType = overrideType;
        }
    }

    public enum ModClassOverrideType
    {
        Class,
        UIScreenListener
    }
}