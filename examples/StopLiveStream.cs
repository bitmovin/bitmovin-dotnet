using Xunit;

namespace com.bitmovin.Api.Examples
{
    public class StopLiveStream
    {
        private const string API_KEY = "YOUR API KEY";
        private const string ENCODING_ID = "LIVE-ENCODING-ID";

        [Fact]
        public void StartLiveEncoding()
        {
            var bitmovin = new BitmovinApi(API_KEY);
            bitmovin.Encoding.Encoding.StopLive(ENCODING_ID);
        }


    }
}