using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Rest
{
    public class Detail
    {
        public string Date { get; set; }

        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType? Type { get; set; }

        public string Text { get; set; }

        public string Field { get; set; }

        public List<Link> Links { get; set; }
    }
}