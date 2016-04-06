using System.Collections.Generic;

namespace XCOM2Launcher.Mod
{
    public class ModConflict
    {
        public string ClassName { get; private set; }
        public IEnumerable<ModClassOverride> Overrides { get; private set; }

        public ModConflict(string className, IEnumerable<ModClassOverride> overrides)
        {
            ClassName = className;
            Overrides = overrides;
        }
    }
}
