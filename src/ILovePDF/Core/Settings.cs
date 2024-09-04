using System;
using System.Diagnostics;
using System.Reflection;

namespace iLovePdf.Core
{
    /// <summary>
    ///     ILovePDF Wrapper settings
    /// </summary>
    internal static class Settings
    {
        public const String StartUrl = @"https://api.ilovepdf.com";

        public const String Host = @"https://";

        public const String V1 = @"v1";

        //2MB 2000000
        public const Int32 MaxBytesPerChunk = 2000000;

        public static String NetVersion => FileVersionInfo
            .GetVersionInfo(
#if NETSTANDARD1_5
                typeof(Settings).GetTypeInfo().Assembly.Location
#else
                Assembly.GetExecutingAssembly().Location
#endif
                ).FileVersion;
    }

    internal static class StringHelpers
    {
        internal static String Invariant(String v)
        {
#if NETSTANDARD1_5
            return v;
#else
            return v.ToString(System.Globalization.CultureInfo.InvariantCulture);
#endif
        }
    }
}