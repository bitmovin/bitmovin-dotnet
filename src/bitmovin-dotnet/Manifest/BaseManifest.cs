using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Manifest
{
    public class BaseManifest : BaseObject
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ManifestType? Type { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}