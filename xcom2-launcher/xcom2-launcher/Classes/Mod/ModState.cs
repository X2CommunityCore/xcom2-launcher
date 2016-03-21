using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XCOM2Launcher.Mod
{
    [Flags]
    public enum ModState
    {
        None = 0,
        New = 1,

        NotInstalled = 2,
        UpdateAvailable = 4,

        ModConflict = 8,
        DuplicateID = 16,
    }
}
