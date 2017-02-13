using System.Collections.Generic;
using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Manifest
{
    public class MediaInfo : BaseObject
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MediaType? Type { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string GroupId { get; set; }

        public string Language { get; set; }

        public string AssocLanguage { get; set; }

        public bool? IsDefault { get; set; }

        public bool? Autoselect { get; set; }

        public bool? Forced { get; set; }

        public string InstreamId { get; set; }

        public List<string> Characteristics { get; set; }

        public string SegmentPath { get; set; }

        public string EncodingId { get; set; }

        public string StreamId { get; set; }

        public string MuxingId { get; set; }

        public string DrmId { get; set; }

        public int? StartSegmentNumber { get; set; }

        public int? EndSegmentNumber { get; set; }
    }
}