using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Core.Sign
{
    public class FileResponse
    { 
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("filesize")]
        public int Filesize { get; set; } 
    }
}
