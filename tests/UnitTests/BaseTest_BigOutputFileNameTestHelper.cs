using iLovePdf.Model.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System; 

namespace Tests
{
    public abstract partial class BaseTest
    { 
        public string Arrange_BigOutputFileName()
        {  
            var outputFileName = @"";
            for (var i = 0; i < Settings.MaxCharactersInFilename; i++)
            {
                outputFileName = $"{outputFileName}a";
            }
            return $"{outputFileName}.pdf"; 
        }

        public void AssertThrowsException_BigOutputFileName(Func<object> action)
        {
            Assert.ThrowsException<ServerErrorException>(
                action: action, 
                message: "OutputFileName bigger than allowed was inappropriately processed.");
        }
    }
}
