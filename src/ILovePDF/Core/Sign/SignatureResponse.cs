using Newtonsoft.Json; 
using System.Collections.Generic; 
using LovePdf.Core.Sign;

namespace LovePdf.Core.Sign
{
    public class SignatureResponse
    {
        [JsonProperty("about_to_expire_reminder")]
        public bool AboutToExpireReminder { get; set; }

        [JsonProperty("completed_on")]
        public object CompletedOn { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("disable_notifications")]
        public object DisableNotifications { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("expires")]
        public string Expires { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("lock_order")]
        public bool LockOrder { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }

        [JsonProperty("signer_reminder_days_cycle")]
        public int SignerReminderDaysCycle { get; set; }

        [JsonProperty("signer_reminders")]
        public bool SignerReminders { get; set; }

        [JsonProperty("subject_cc")]
        public object SubjectCc { get; set; }

        [JsonProperty("subject_signer")]
        public object SubjectSigner { get; set; }

        [JsonProperty("timezone")]
        public object Timezone { get; set; }

        [JsonProperty("token_requester")]
        public string TokenRequester { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("uuid_visible")]
        public bool UuidVisible { get; set; }

        [JsonProperty("verify_enabled")]
        public bool VerifyEnabled { get; set; }

        [JsonProperty("expired")]
        public bool Expired { get; set; }

        [JsonProperty("expiring")]
        public bool Expiring { get; set; }

        [JsonProperty("certified")]
        public bool Certified { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("signers")]
        public List<SignerResponse> Signers { get; set; }

        [JsonProperty("files")]
        public List<FileResponse> Files { get; set; }
    }
}
