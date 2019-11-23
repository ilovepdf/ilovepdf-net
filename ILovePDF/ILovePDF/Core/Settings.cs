using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace LovePdf.Core
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
            .GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
    }

    internal static class StringHelpers
    {
        internal static String Invariant(String v)
        {
            return v.ToString(CultureInfo.InvariantCulture);
        }
    }
}