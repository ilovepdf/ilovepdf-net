using LovePdf.Core;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Sign.Elements;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class SignBasic
    {
        public async Task DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            // Create sign task
            var task = api.CreateTask<SignTask>();

            // File variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            // Create task params
            var signParams = new SignParams();

            // Create a signer
            var signer = signParams.AddSigner("Signer", "signer@email.com");

            // Add file that a receiver of type signer needs to sign.
            var signerFile = signer.AddFile(file.ServerFileName);

            // Add signers and their elements;
            var signatureElement = signerFile.AddSignature();
            signatureElement.Position = new Position("20", "-20");
            signatureElement.Pages = "1";
            signatureElement.Size = 40;

            // Lastly send the signature request
            var signature = await task.RequestSignatureAsync(signParams);
        }
    }
}