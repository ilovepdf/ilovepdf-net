﻿using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;

namespace LovePdf.Model.TaskParams.Sign.Signers
{
    /// <inheritdoc cref="SignSignerType.Signer"/>
    public class Signer : BaseSignSigner
    {
        public Signer(string name, string email) : base(SignSignerType.Signer, name, email)
        {
        }

        /// <summary> 
        /// Signer phone. Leave it empty this property if 
        /// you don't want to activate the SMS validation feature. 
        /// It is only valid for type signer.
        /// <para>NOTE: For every signer with phone, SMS credit will be 
        /// consumed, please check the following link to see how credit 
        /// consumption works.</para>
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary> 
        /// Use this field if you want to force this a receiver of type 
        /// signer to use a specific signature format.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("force_signature_type")]
        public ForceSignatureType ForceSignatureType { set; get; } = ForceSignatureType.All;

        /// <summary> 
        /// Files that a receiver of type signer needs to sign.
        /// </summary>
        [JsonIgnore] // [JsonProperty("files")]
        public List<SignerFile> Files { get; set; } = new List<SignerFile>();

        /// <summary>
        /// File that a receiver of type signer needs to sign.
        /// </summary>
        /// <param name="serverFileName"></param>
        /// <returns></returns>
        public SignerFile AddFile(string serverFileName)
        {
            var file = new SignerFile();
            file.ServerFilename = serverFileName;
            Files.Add(file);
            return file;
        }
    }
}
