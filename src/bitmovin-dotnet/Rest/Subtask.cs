using System.Collections.Generic;
using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Rest
{
    public class Subtask
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Status? Status { get; set; }

        public string Name { get; set; }

        public double? Eta { get; set; }

        public int? Progress { get; set; }

        public List<Message> Messages { get; set; }
    }
}