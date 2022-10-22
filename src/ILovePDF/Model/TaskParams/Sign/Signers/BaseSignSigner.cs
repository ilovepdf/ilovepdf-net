using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Elements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign.Signers
{
    public class BaseSignSigner : ISignSigner
    {
        public BaseSignSigner(SignSignerType signerType, string name, string email)
        {
            this.Type = signerType;
            this.Name = name;
            this.Email = email;
        }

        /// <summary> 
        /// Receiver email.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public SignSignerType Type { get; private set; }

        /// <summary> 
        /// Receiver full name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary> 
        /// Receiver email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary> 
        /// If provided, all receivers will have to insert an access code. 
        /// Otherwise the receiver will not be able to access the document
        /// </summary>
        [JsonProperty("access_code")]
        public string AccessCode { get; set; } 
    }
}
