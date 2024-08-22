using System;
using System.IO;

namespace Tests
{
    /// <summary>
    ///     ILovePDF Test settings
    /// </summary>
    internal static class Settings
    {
        public const String RightPublicKey =
            @"project_public_c0ac272c966a051c024a9efcd05e0837_lGE_M521c52f2c0421da14164986b6a281270";

        public const String WrongPublicKey = @"wrong";

        public const String RightSecretKey =
            @"secret_key_7f1d69768ded72ab078cd21f555088b3_PUd5_0ecef4507c55c30a28c22bd42ce4f408";

        public const String WrongSecretKey = @"wrong";
        public const String WaterMarkText = @"WATERMARK";
        public const String WrongEncryptionKey = @"0123456789012345aaa"; // 19 chars
        public const String RightEncryptionKey = @"0123456789012345"; // 16 chars    
        public const String RightPassword = @"0123456789012345";
        public const String WrongPassword = @"wrong";
        public const String GoodWordFile = @"should-work.doc";
        public const String BadWordFile = @"should-fail.doc";
        public const String GoodWordUrl = @"https://www.idee.org/Georgia_opposition_NATO-Eng-F.doc";
        public const String GoodExcelFile = @"should-work.xlsx";
        public const String BadExcelFile = @"should-fail.xlsx";
        public const String GoodJpgUrl = @"https://upload.wikimedia.org/wikipedia/en/a/a9/Example.jpg";
        public const String GoodJpgFile = @"should-work.jpg";
        public const String BadJpgFile = @"should-fail.jpg";
        public const String GoodPngFile = @"should-work.png";
        public const String BadPngFile = @"should-fail.png";
        public const String GoodTiffFile = @"should-work.tiff";
        public const String BadTiffFile = @"should-fail.tiff";
        public const String GoodPdfUrl = @"https://hutchesonlab.fiu.edu/wp-content/uploads/sample-pdf.pdf";
        public const String GoodHtmlUrl = @"http://www.orimi.com/";
        public const String BadHtmlUrl = @"http://www.orimi.com:8888/";


        public const String GoodMultipagePdfUrl =
            @"https://www.adobe.com/support/products/enterprise/knowledgecenter/media/c4611_sample_explain.pdf";

        public const String GoodPdfFile = @"should-work.pdf";
        public const String GoodMultipagePdfFile = @"should-work-multipage.pdf";
        public const String BadPdfFile = @"should-fail.pdf";
        public const String WrongFontColor = @"wrong";
        public const String WrongPages = @"wrong";
        public const String GoodPdfFilePasswordProtected = @"should-work-password-protected-0123456789012345.pdf";

        public const String GoodMultipagePdfFilePasswordProtected =
            @"should-work-multipage-password-protected-0123456789012345.pdf";

        public const Int32 MaxAllowedFiLes = 200;
        public const Int32 MaxCharactersInFilename = 130;
        public const Int32 TimeoutSeconds = 60;
        public const String DefaultMultipageOutput = "output.zip";
        public const String DefaultSinglepageOutput = "sample-pdf.pdf";
        public static String BasePath => $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}";
        public static String DataPath => $"{BasePath}{Path.DirectorySeparatorChar}Data";
    }
}