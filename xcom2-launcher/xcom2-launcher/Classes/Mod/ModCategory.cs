using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM2Launcher.Mod
{
    public class ModCategory
    {
        public bool Collapsed { get; set; } = false;

        public List<ModEntry> Entries { get; set; } = new List<ModEntry>();


        public ModCategory() { }
        
    }
}
