namespace XCOM2Launcher.Mod
{
    public class ModClassOverride
    {
        public string NewClass;
        public string OldClass;
        public ModClassOverrideType OverrideType;
    }

    public enum ModClassOverrideType
    {
        Class,
        UIScreenListener
    }
}