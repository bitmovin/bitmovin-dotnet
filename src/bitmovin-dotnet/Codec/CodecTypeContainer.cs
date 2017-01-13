using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Codec
{
    public class CodecTypeContainer
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CodecType? Type { get; set; }
    }
}