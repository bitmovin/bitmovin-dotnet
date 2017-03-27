using System.Collections.Generic;
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

        [JsonConverter(typeof(StringEnumConverter))]
        public BAdapt? BAdaptiveStrategy { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public H264MotionEstimationMethod? MotionEstimationMethod { get; set; }

        public int? RcLookahead { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public H264SubMe? SubMe { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public H264Trellis? Trellis { get; set; }
        
        public List<H264Partition> Partitions { get; set; }

        public int? Slices { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public H264InterlaceMode? InterlaceMode { get; set; }
    }

}