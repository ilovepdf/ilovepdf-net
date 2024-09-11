using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Core.Sign
{ 
    public class ReceiverInfoResponse
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public object Phone { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token_requester")]
        public string TokenRequester { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("access_code")]
        public bool AccessCode { get; set; }

        [JsonProperty("force_signature_type")]
        public string ForceSignatureType { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }
    }

}
