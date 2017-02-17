using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Encoding
{
    public class Encoding : BaseObject
    {
        public string CreatedAt { get; set; }

        public string ModifiedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string EncoderVersion { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EncodingCloudRegion? CloudRegion { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EncodingStatus? Status { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EncodingType? Type { get; set; }
    }
}