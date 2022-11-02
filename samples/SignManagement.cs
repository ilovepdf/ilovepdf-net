using LovePdf.Core;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams.Sign;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class SignManagement
    {
        public async Task DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            // Create sign management task
            var request = api.CreateTask<SignTask>();

            string signatureToken = "signaturetoken";
            string receiverToken = "receivertoken";

            // Get a list of all created signature requests
            var signaturesList = await request.GetSignaturesAsync();

            // Get the first page, with max number of 50 entries
            // per page (default is 20, max is 100).
            var signaturesListFirstPage =
                await request.GetSignaturesAsync(new ListRequest(0, 50));

            // Get the current status of the signature:
            var signatureStatus =
                await request.GetSignatureStatusAsync(signatureToken);

            // Get information about a specific receiver:
            var receiverInfo =
                await request.GetReceiverInfoAsync(receiverToken);

            // Download the audit file on the filesystem
            var auditFilePath = 
                await request.DownloadAuditFileAsync(signatureToken, "./save_path");

            // Download the original files on the filesystem:
            // It downloads a PDF file if a single file was uploaded. 
            // Otherwise a zip file with all uploaded files is downloaded.
            var originalFilesPath = 
                await request.DownloadOriginalFilesAsync(signatureToken, "./save_path");

            // Download the created signed files on the filesystem:
            // It downloads a PDF file if a single file was uploaded. 
            // Otherwise a zip file with all uploaded files is downloaded.
            var signedFilesPath = 
                await request.DownloadSignedFilesAsync(signatureToken, "./save_path");

            // Correct the email address of a receiver in
            // the event that the email was delivered to an
            // invalid email address
            var fixReceiverEmailResult =
                await request.FixReceiverEmailAsync(receiverToken, "newemail@email.Com");

            // Correct the mobile number of a signer in the
            // event that the SMS was delivered to an invalid
            // mobile number
            var fixSignerPhoneResult =
                await request.FixSignerPhoneAsync(receiverToken, "34666666666");

            // This endpoint sends an email reminder to pending
            // receivers. It has a daily limit quota (check the
            // docs to know the daily quota)
            var sendRemindersResult =
                await request.SendRemindersAsync(signatureToken);

            // Increase the number of days to '4' in order to
            // prevent the request from expiring and give receivers
            // extra time to perform remaining actions.
            var increaseExpirationDaysResult =
                await request.IncreaseExpirationDaysAsync(signatureToken, 4);

            // Void a signature that is currently in progress
            var voidSignatureResult =
                await request.VoidSignatureAsync(signatureToken);
        }
    }
}