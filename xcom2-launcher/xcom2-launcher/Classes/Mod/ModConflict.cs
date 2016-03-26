using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Classes.Mod
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
