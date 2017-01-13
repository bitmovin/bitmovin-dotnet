using System.Collections.Generic;
using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Encoding
{
    public class InputStream
    {
        public string InputId { get; set; }

        public string InputPath { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectionMode? SelectionMode { get; set; }

        public int? Position { get; set; }
    }

    public class Stream : BaseObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<InputStream> InputStreams { get; set; }

        public string CodecConfigId { get; set; }

        public List<Output> Outputs { get; set; }
    }
}