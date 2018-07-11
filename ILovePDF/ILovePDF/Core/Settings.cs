using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace LovePdf.Core
{
    /// <summary>
    /// ILovePDF Wrapper settings
    /// </summary>
    internal static class Settings
    {
        public const string StartUrl = @"https://api.ilovepdf.com";
        public const string Host = @"https://";

        public const string V1 = @"v1";

        //2MB 2000000
        public const int MaxBytesPerChunk = 2000000;

        public static string NetVersion
        {
            get
            {
                var version = @"unknown_version";
                var assembly = Assembly.GetExecutingAssembly();

                if (assembly.Location == null)
                    return version;

                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                version = fvi.FileVersion;

                return version;
            }
        }
    }

    internal static class StringHelpers
    {
        internal static string Invariant(string v) => v.ToString(CultureInfo.InvariantCulture);
    }

}
