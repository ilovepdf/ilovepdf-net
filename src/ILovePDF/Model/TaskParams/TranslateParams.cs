using Newtonsoft.Json;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    /// Parameters for Translate task
    /// </summary>
    public class TranslateParams : BaseParams
    {
        [JsonProperty("language_output")]
        public string LanguageOutput { get; set; }

        [JsonProperty("translate_mode")]
        public string TranslateMode { get; set; }
    }
}
