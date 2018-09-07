using System.Collections.Generic;
using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Encoding
{
    public class Stream : BaseObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<InputStream> InputStreams { get; set; }

        public string CodecConfigId { get; set; }

        public List<Output> Outputs { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StreamMode? Mode { get; set; }
    }
}