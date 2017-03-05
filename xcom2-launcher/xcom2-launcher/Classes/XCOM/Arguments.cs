using System.ComponentModel;

namespace XCOM2Launcher.XCOM
{
    public class Arguments
    {
        [DisplayName("-review")]
        [Description("Final main menu? \r\nDefault: true")]
        [DefaultValue(true)]
        public bool review { get; set; } = true;

        [DisplayName("-noRedScreens")]
        [Description("Hide error popups?\r\nDefault: true")]
        [DefaultValue(true)]
        public bool noRedScreens { get; set; } = true;

        [DisplayName("-log")]
        [Description("Show log console?\r\nDefault: false")]
        [DefaultValue(false)]
        public bool log { get; set; } = false;

        [DisplayName("-crashDumpWatcher")]
        [Description("No idea.\r\nDefault: true")]
        [DefaultValue(true)]
        public bool crashDumpWatcher { get; set; } = true;

        [DisplayName("-noStartUpMovies")]
        [Description("Skip intro movies?\r\nDefault: false")]
        [DefaultValue(false)]
        public bool noStartupMovies { get; set; } = false;

        [DisplayName("-language=")]
        [Description("Force language\r\nDefault: empty")]
        [DefaultValue("")]
        public string Language { get; set; } = "";


        [DisplayName("-allowConsole")]
        [Description("Allow cheat console?\r\nDefault: false")]
        [DefaultValue(false)]
        public bool allowConsole { get; set; } = false;

        [DisplayName("-autoDebug")]
        [Description("No idea.\r\nDefault: false")]
        [DefaultValue(false)]
        public bool autoDebug { get; set; } = false;

	    [DisplayName("-noSeekFreeLoading")]
	    [Description("No idea\r\nDefault: false")]
	    [DefaultValue(false)]
	    public bool noSeekFreeLoading { get; set; } = false;

        [DisplayName("Additional arguments")]
        [Description("Add something extra")]
        [DefaultValue(null)]
        public string Custom { get; set; } = null;

        public override string ToString()
        {
            string args = "-fromLauncher";

            if (log)
                args += " -log";

            if (review)
                args += " -review";

            if (Language.Length > 0)
                args += " -language=" + Language;

            if (noRedScreens)
                args += " -noRedScreens";

            if (noStartupMovies)
                args += " -noStartupMovies";

            if (crashDumpWatcher)
                args += " -CrashDumpWatcher";

            if (allowConsole)
                args += " -allowConsole";

            if (autoDebug)
                args += " -autoDebug";

	        if (noSeekFreeLoading)
		        args += "-noseekfreeloading";

			if (!string.IsNullOrEmpty(Custom))
                args += " " + Custom;


            return args;
        }
    }
}
