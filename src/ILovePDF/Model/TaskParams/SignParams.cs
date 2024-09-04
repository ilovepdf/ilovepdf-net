using iLovePdf.Attributes;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams.Sign.Elements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    /// EditParams
    /// </summary>
    public partial class SignParams : BaseParams
    {
        private int expirationDays = 120;

        public SignParams(List<ISignSigner> signers = null)
        {
            if (signers != null)
            {
                Signers = signers;
            }
        }

        /// <summary>
        /// Name of your brand. Update value using "SetBrand" method.
        /// </summary> 
        [JsonProperty("brand_name")]
        public string BrandName { get; set; }

        /// <summary>
        /// Server filename of the uploaded logo file.
        /// </summary> 
        [JsonProperty("brand_logo")]
        public string BrandLogo { get; set; }

        /// <summary>
        /// Receivers will receive emails in the provided language.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("language")]
        public Languages Language { get; set; } = Languages.English;

        /// <summary>
        /// <para><b>false</b> = allows receivers of signer type to sign in parallel.</para>
        /// <para><b>true</b> = receivers of signer type must sign sequentially in order.</para>
        /// </summary>
        [JsonProperty("lock_order")]
        [JsonConverter(typeof(BoolConverter))]
        public bool LockOrder { get; set; }

        /// <summary> 
        /// <para><b>false</b> = No email reminders will be sent to any receivers.</para>
        /// <para><b>true</b> = Email reminders are sent periodically to receivers.</para>
        /// <para>See also <see cref="SignerReminderDaysCycle" /> </para>
        /// </summary>
        [JsonProperty("signer_reminders")]
        public bool SignerReminders { get; set; } = true;

        /// <summary>
        /// It is the time period that will wait between each reminder.
        /// <para>See also <see cref="SignerReminders" /> </para>
        /// </summary>
        [JsonProperty("signer_reminder_days_cycle")]
        public int SignerReminderDaysCycle { get; set; } = 1;

        /// <summary> 
        /// <para><b>false</b> = No QR code is added on the audit trail.</para>
        /// <para><b>true</b> =  It adds a QR code on the audit trail 
        /// that leads you to the original documents and signed documents 
        /// where you are free to download them.</para>
        /// </summary>
        [JsonProperty("verify_enabled")]
        public bool VerifyEnabled { get; set; } = true;

        /// <summary>
        /// It is highly recommend to enable this, otherwise it lowers the signatures validity.
        /// <para><b>false</b> = uuid is not shown at the bottom of each signature.</para>
        /// <para><b>true</b> = uuid is shown at the bottom of each signature element.</para>
        /// </summary>
        [JsonProperty("uuid_visible")]
        public bool UuidVisible { get; set; } = true;

        /// <summary>
        /// Days until the signature request will expire. (Value must be between 1 and 130)
        /// </summary>
        [JsonProperty("expiration_days")]
        public int ExpirationDays
        {
            get => expirationDays;
            set
            {
                if (value < 1 || value > 130)
                {
                    throw new ArgumentOutOfRangeException(nameof(expirationDays), "Value must be between 1 and 130");
                }
                expirationDays = value;
            }
        }

        /// <summary>
        /// Message Signer. Use this if you want to change the body of the initial email sent to the receivers.
        /// </summary>
        [JsonProperty("message_signer")]
        public string MessageSigner { get; set; }

        /// <summary>
        /// SubjectSigner. Use this if you want to change the subject of the initial email sent to the receivers.
        /// </summary>
        [JsonProperty("subject_signer")]
        public string SubjectSigner { get; set; }

        /// <summary>
        /// Receivers that participate in the signature process. 
        /// </summary>
        [JsonIgnore]
        public List<ISignSigner> Signers { get; private set; } = new List<ISignSigner>();
         
        /// <summary>
        /// Displayed brand name and logo in email notifications. 
        /// </summary>
        /// <param name="name">Name of your brand.</param>
        /// <param name="logoServerFileName">Server filename of the uploaded logo file.</param>
        /// <returns></returns>
        public SignParams SetBrand(string name, string serverFileName)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(serverFileName))
            {
                throw new ArgumentNullException(nameof(name), "Parameters are mandatory.");
            }         

            this.BrandName = name;
            this.BrandLogo = serverFileName;

            return this;
        }
    }
}