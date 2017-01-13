using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Encoding
{
    public class Encoding : BaseObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string EncoderVersion { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EncodingCloudRegion? CloudRegion { get; set; }
    }
}