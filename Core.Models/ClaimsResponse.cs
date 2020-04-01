using Newtonsoft.Json;

namespace Core.Models
{
    public class ClaimsResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}