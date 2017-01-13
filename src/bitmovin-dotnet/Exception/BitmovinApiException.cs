using System.Net.Http;
using Newtonsoft.Json;

namespace com.bitmovin.Api.Exception
{
    public class BitmovinApiException : System.Exception
    {
        private readonly string _content;

        public BitmovinApiException(HttpResponseMessage response)
        {
            _content = response.Content.ReadAsStringAsync().Result;
        }

        public BitmovinWebExceptionMessage ExceptionMessage
            => JsonConvert.DeserializeObject<BitmovinWebExceptionMessage>(_content);
    }
}