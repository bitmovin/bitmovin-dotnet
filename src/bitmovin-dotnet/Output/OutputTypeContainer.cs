using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Output
{
    public class OutputTypeContainer
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public OutputType Type { get; set; }
    }
}