using com.bitmovin.Api.Encoding.PerTitleEncoding;
using com.bitmovin.Api.Enums;
using System;

namespace com.bitmovin.Api.Encoding
{
    public class StartEncodingRequest
    {
        [Obsolete("Use Trimming.Offset instead")]
        public double? Offset { get; set; }

        [Obsolete("Use Trimming.Duration instead")]
        public double? Duration { get; set; }

        public Trimming Trimming { get; set; }

        public EncodingMode? EncodingMode { get; set; }

        public PerTitle PerTitle { get; set; }
    }
}