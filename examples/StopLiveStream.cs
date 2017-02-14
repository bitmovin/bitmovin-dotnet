using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace com.bitmovin.Api.Examples
{
    [TestClass]
    public class StopLiveStream
    {
        private const string API_KEY = "YOUR API KEY";
        private const string ENCODING_ID = "LIVE-ENCODING-ID";

        [TestMethod]
        public void StopLiveEncoding()
        {
            var bitmovin = new BitmovinApi(API_KEY);
            bitmovin.Encoding.Encoding.StopLive(ENCODING_ID);
        }


    }
}