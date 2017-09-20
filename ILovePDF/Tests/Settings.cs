using System;
using System.IO;

namespace Tests
{
    /// <summary>
    /// ILovePDF Test settings
    /// </summary>
    internal static class Settings
    {
        public const string RightPublicKey = @"project_public_3fedaeb8b6ff0e34a849c049422f4725_ZQHs-56404af1d4525a22201eeacc0b8e4ed0";
        public const string WrongPublicKey = @"wrong";
        public const string RightSecretKey = @"secret_key_e6e42ebc47aaa75f7161b307a3464297__tMrk149492f181e7a12e0eae08527cfa166a";
        public const string WrongSecretKey = @"wrong";
        public static string BasePath => $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}";
        public static string DataPath => $"{BasePath}{Path.DirectorySeparatorChar}Data";
        public const string WaterMarkText = @"WATERMARK";
        public const string WrongEncryptionKey = @"0123456789012345aaa"; // 19 chars
        public const string RightEncryptionKey = @"0123456789012345"; // 16 chars    
        public const string RightPassword = @"0123456789012345";
        public const string WrongPassword = @"wrong";
        public const string GoodWordFile = @"should-work.doc";
        public const string BadWordFile = @"should-fail.doc";
        public const string GoodWordUrl = @"https://www.idee.org/Georgia_opposition_NATO-Eng-F.doc";
        public const string GoodExcelFile = @"should-work.xlsx";
        public const string BadExcelFile = @"should-fail.xlsx";
        public const string GoodJpgUrl = @"https://upload.wikimedia.org/wikipedia/en/a/a9/Example.jpg";
        public const string GoodJpgFile = @"should-work.jpg";
        public const string BadJpgFile = @"should-fail.jpg";
        public const string GoodPngFile = @"should-work.png";
        public const string BadPngFile = @"should-fail.png";
        public const string GoodTiffFile = @"should-work.tiff";
        public const string BadTiffFile = @"should-fail.tiff";
        public const string GoodPdfUrl = @"http://www.orimi.com/pdf-test.pdf";
        public const string GoodMultipagePdfUrl = @"https://www.ets.org/Media/Tests/GRE/pdf/gre_research_validity_data.pdf";
        public const string GoodPdfFile = @"should-work.pdf";
        public const string GoodMultipagePdfFile = @"should-work-multipage.pdf";
        public const string BadPdfFile = @"should-fail.pdf";
        public const string WrongFontColor = @"wrong";
        public const string WrongPages = @"wrong";
        public const string GoodPdfFilePasswordProtected = @"should-work-password-protected-0123456789012345.pdf";
        public const string GoodMultipagePdfFilePasswordProtected = @"should-work-multipage-password-protected-0123456789012345.pdf";
        public const int MaxAllowedFiLes = 200;
        public const int MaxCharactersInFilename = 130;
        public const int TimeoutSeconds = 60;
        public const string DefaultMultipageOutput = "output.zip";
        public const string DefaultSinglepageOutput = "result.pdf";
    }
}
