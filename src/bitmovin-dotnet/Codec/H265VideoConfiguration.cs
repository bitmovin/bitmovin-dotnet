using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Codec
{
    public class H265VideoConfiguration : VideoConfiguration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public H265Profile? Profile { get; set; }

        public int? BFrames { get; set; }

        public int? RefFrames { get; set; }

        public int? Qp { get; set; }

        public int? MaxBitrate { get; set; }

        public int? MinBitrate { get; set; }

        public int? Bufsize { get; set; }

        public int? MinGop { get; set; }

        public int? MaxGop { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public H265Level? Level { get; set; }

        public int? RcLookahead { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BAdapt? Badapt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MaxCtuSize? MaxCtuSize { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TuIntraDepth? TuIntraDepth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TuInterDepth? TuInterDepth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MotionSearch? MotionSearch { get; set; }

        public int? SubMe { get; set; }

        public int? MotionSearchRange { get; set; }

        public bool? WeightPredictionOnPSlice { get; set; }

        public bool? WeightPredictionOnBSlice { get; set; }

        public bool? Sao { get; set; }
    }
}