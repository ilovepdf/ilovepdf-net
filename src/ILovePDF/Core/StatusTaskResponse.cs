using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    /// <summary>
    ///     Status Task Response
    /// </summary>
    public class StatusTaskResponse
    {
        [JsonProperty("tool")]
        public string Tool { get; set; }

        [JsonProperty("process_start")]
        public string ProcessStart { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("timer")]
        public string Timer { get; set; }

        [JsonProperty("filesize")]
        public int Filesize { get; set; }

        [JsonProperty("output_filesize")]
        public int OutputFilesize { get; set; }

        [JsonProperty("output_filenumber")]
        public int OutputFilenumber { get; set; }

        [JsonProperty("output_extensions")]
        public List<string> OutputExtensions { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("task")]
        public string Task { get; set; }

        [JsonProperty("file_number")]
        public string FileNumber { get; set; }

        [JsonProperty("download_filename")]
        public string DownloadFilename { get; set; }

        [JsonProperty("files")]
        public List<StatusTaskFileResponse> Files { get; set; }
    }

    public class StatusTaskFileResponse 
    {
        [JsonProperty("server_filename")]
        public string ServerFilename { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("timer")]
        public double Timer { get; set; }

        [JsonProperty("filesize")]
        public int Filesize { get; set; }

        [JsonProperty("output_filesize")]
        public int OutputFilesize { get; set; }
    }
}