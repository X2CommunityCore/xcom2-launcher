using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XCOM2Launcher.Classes.Helper {
    static class GitVersionInfo
    {
        public static readonly Version Version;
        public static readonly string SemVer;
        public static readonly string FullSemVer;
        public static readonly string Sha;
        public static readonly string MajorMinorPatch;
        public static readonly string PreReleaseTag;
        public static readonly string PreReleaseLabel;
        public static readonly int PreReleaseNumber;
        public static readonly int Major;
        public static readonly int Minor;
        public static readonly int Patch;
        
        static GitVersionInfo() {
            var assembly = Assembly.GetExecutingAssembly();
            var fields = assembly.GetType("GitVersionInformation").GetFields();

            int.TryParse(fields.Single(f => f.Name == "Major").GetValue(null).ToString(), out Major);
            int.TryParse(fields.Single(f => f.Name == "Minor").GetValue(null).ToString(), out Minor);
            int.TryParse(fields.Single(f => f.Name == "Patch").GetValue(null).ToString(), out Patch);
            int.TryParse(fields.Single(f => f.Name == "PreReleaseNumber").GetValue(null).ToString(), out PreReleaseNumber);
            
            SemVer = fields.Single(f => f.Name == "SemVer").GetValue(null).ToString();
            FullSemVer = fields.Single(f => f.Name == "FullSemVer").GetValue(null).ToString();
            Sha = fields.Single(f => f.Name == "Sha").GetValue(null).ToString();
            MajorMinorPatch = fields.Single(f => f.Name == "MajorMinorPatch").GetValue(null).ToString();
            PreReleaseTag = fields.Single(f => f.Name == "PreReleaseTag").GetValue(null).ToString();
            PreReleaseLabel = fields.Single(f => f.Name == "PreReleaseLabel").GetValue(null).ToString();
            
            Version = new Version(MajorMinorPatch);
        }
    }
}
