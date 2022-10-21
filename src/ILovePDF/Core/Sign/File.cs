using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Core.Sign
{
    public class File
    { 
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("filesize")]
        public int Filesize { get; set; } 
    }
}
