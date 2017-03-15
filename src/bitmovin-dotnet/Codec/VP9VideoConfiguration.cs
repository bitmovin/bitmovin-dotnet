using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Codec
{
    public class VP9VideoConfiguration : VideoConfiguration
    {

        public int? LagInFrames { get; set; }
        public int? TileColumns { get; set; }
        public int? TileRows { get; set; }
        public bool? FrameParallel { get; set; }
        public long? MaxIntraRate { get; set; }
        public int? QpMin { get; set; }
        public int? QpMax { get; set; }
        public int? Crf { get; set; }
        public int? RateUndershootPct { get; set; }
        public int? RateOvershootPct { get; set; }
        public int? CpuUsed { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VP9Quality? Quality { get; set; }

        public bool? NoiseSensitivity { get; set; }
        public bool? Lossless { get; set; }
        public int? StaticThresh { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VP9AqMode? AqMode { get; set; }

        public int? ArnrMaxFrames { get; set; }
        public int? ArnrStrength { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VP9ArnrType? ArnrType { get; set; }
    }
}