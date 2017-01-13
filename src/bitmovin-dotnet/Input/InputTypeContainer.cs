using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Input
{
    public class InputTypeContainer
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public InputType Type { get; set; }
    }
}