using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Rest
{
    public class Data<T>
    {
        public T Result { get; set; }

        public List<Message> Messages { get; set; }

        public int? Code { get; set; }

        public string Message { get; set; }

        public string DeveloperMessage { get; set; }

        public List<Link> Links { get; set; }

        public List<Detail> Details { get; set; }
    }

    public class ResponseEnvelope<T>
    {
        public string RequestId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseStatus? Status { get; set; }

        public Data<T> Data { get; set; }
    }
}