using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Codec
{
    public class H264VideoConfiguration : VideoConfiguration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public H264Profile? Profile { get; set; }

        public int? BFrames { get; set; }

        public int? RefFrames { get; set; }

        public int? QpMin { get; set; }

        public int? QpMax { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MvPredictionMode? MvPredictionMode { get; set; }

        public int? MvSearchRangeMax { get; set; }

        public bool? Cabac { get; set; }

        public int? MaxBitrate { get; set; }

        public int? MinBitrate { get; set; }

        public int? Bufsize { get; set; }

        public int? MinGop { get; set; }

        public int? MaxGop { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public H264Level? Level { get; set; }
    }
}