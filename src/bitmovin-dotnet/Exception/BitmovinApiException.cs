using System;
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

        public BitmovinWebException Exception
            => JsonConvert.DeserializeObject<BitmovinWebException>(_content);

        public override string ToString()
        {
            return String.Format("Exception Message: {0}\r\nRaw Response: {1}", Exception, _content);
        }
    }
}