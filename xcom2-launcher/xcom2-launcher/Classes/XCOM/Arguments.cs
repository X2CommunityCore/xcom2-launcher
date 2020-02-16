using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace XCOM2Launcher.XCOM
{
    public class Argument
    {
        public string Name { get; }
        public string Parameter { get; }
        public string Description { get; }
        public bool IsEnabledByDefault { get; }
        public bool IsDefaultQuickArg { get; }

        public static List<Argument> DefaultArguments;

        static Argument()
        {
            DefaultArguments = new List<Argument>
            {
                new Argument("Review", "-review", "Show final main menu", true, true),
                new Argument("No Red Screens", "-noRedScreens", "Hides error popups", true, true),
                new Argument("Show log console", "-log", "Show log console", false, false),
                new Argument("Crash Dump Watcher", "-crashDumpWatcher", "", false, false),
                new Argument("Skip startup movies", "-noStartUpMovies", "Skip intro movies", false, true),
                new Argument("Set language", "-language=", "Force language", false, false),
                new Argument("Enable console", "-allowConsole", "Enables the debug console", false, true),
                new Argument("Autodebug", "-autoDebug", "", false, false),
                new Argument("No seek free loading ", "-noSeekFreeLoading", "", false, false),
                new Argument("Regenerate ini files", "-regenerateinis", "", false, true)
            };
        }

        public Argument(string name, string parameter, string description = "", bool isEnabledByDefault = false, bool isDefaultQuickArg = false)
        {
            Name = name;
            Parameter = parameter;
            Description = description;
            IsEnabledByDefault = isEnabledByDefault;
            IsDefaultQuickArg = isDefaultQuickArg;
        }
    }
}
