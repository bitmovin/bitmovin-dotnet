using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Codec
{
    public class AACAudioConfiguration : AudioConfiguration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ChannelLayout? ChannelLayout { get; set; }

        public int? VolumeAdjust { get; set; }

        public bool? Normalize { get; set; }
    }
}