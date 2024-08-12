using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Sign.Elements;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class SignAdvanced
    {
        public async Task DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            // Create sign task
            var task = api.CreateTask<SignTask>();

            // We first upload the files that we are going to use
            var file = task.AddFile("path/to/file/document.pdf");

            // Create task params
            var signParams = new SignParams();

            // Set the Signature settings
            signParams.SubjectSigner = "My subject";
            signParams.MessageSigner = "Body of the first message";

            signParams.SignerReminderDaysCycle = 3;
            signParams.SignerReminders = true;
            signParams.ExpirationDays = 130;

            signParams.Language = Languages.English;
            signParams.VerifyEnabled = true;
            signParams.LockOrder = false;
            signParams.UuidVisible = true;

            // Set brand
            signParams.SetBrand("My brand name", new Uri(""), task);
            //signParams.SetBrand("My brand name", "path/to/file/document.jpg", task);

            ///////////////
            // RECEIVERS //
            ///////////////
            // Create the receivers
            var validator = signParams.AddValidator("Validator", "validator@email.com");
            var viewer = signParams.AddViewer("Witness", "witness@email.com");
            var signer = signParams.AddSigner("Signer", "signer@email.com");

            //////////////
            // ELEMENTS //
            //////////////

            // Add file that a receiver of type signer needs to sign.
            var signerFile = signer.AddFile(file.ServerFileName);

            // Add elements to the receivers that need it
            //
            // "Pages" define rules:
            // - we can define the pages with a comma, e.g. "1,2"
            // - ranges can also be defined, e.g. "1-3"
            // - you can define multiple ranges, e.g. "1,2,3-6"

            var signatureElement = signerFile.AddSignature();
            signatureElement.Position = new Position("20", "-20");
            signatureElement.Pages = "1,2";

            var dateElement = signerFile.AddDate("12/12/2022");
            dateElement.Position = new Position("30", "-30");
            dateElement.Pages = "1-3";

            var initialsElement = signerFile.AddInitials();
            initialsElement.Position = new Position("40", "-40");
            initialsElement.Pages = "1,2,3-6";

            var inputElement = signerFile.AddInput();
            inputElement.Position = new Position("50", "-50");
            inputElement.Label = "Passport Number";
            inputElement.Description = "Please put your passport number";
            inputElement.Pages = "1";

            var nameElement = signerFile.AddName();
            nameElement.Position = new Position("60", "-60");
            nameElement.Size = 40;
            nameElement.Pages = "1";

            var textElement = signerFile.AddText("This is a text field");
            textElement.Position = new Position("70", "-70");
            textElement.Size = 40;
            textElement.Pages = "1";

            // Lastly send the signature request
            var signature = await task.RequestSignatureAsync(signParams);
        }
    }
}