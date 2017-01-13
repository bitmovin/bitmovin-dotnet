using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Rest
{
    public class Result<T>
    {
        public int? TotalCount { get; set; }
        public string Previous { get; set; }

        public string Next { get; set; }

        public List<T> Items { get; set; }
    }

    public class PaginationData<T>
    {
        public Result<T> Result { get; set; }
    }

    public class PaginationResponse<T>
    {
        public string RequestId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseStatus? Status { get; set; }

        public PaginationData<T> Data { get; set; }
    }
}