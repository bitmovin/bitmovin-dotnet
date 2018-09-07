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
}
