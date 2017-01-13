using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Manifest
{
    public class Fmp4 : BaseObject
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SegmentScheme? Type { get; set; }

        public string EncodingId { get; set; }

        public string MuxingId { get; set; }

        public int? StartSegmentNumber { get; set; }

        public int? EndSegmentNumber { get; set; }

        public string SegmentPath { get; set; }
    }
}