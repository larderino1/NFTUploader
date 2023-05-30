using Newtonsoft.Json;

namespace NFTUploaderWeb.Models
{
    public class IPFSMetadataModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
