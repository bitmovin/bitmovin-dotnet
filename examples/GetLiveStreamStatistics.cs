using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace com.bitmovin.Api.Examples
{

    [TestClass]
    public class GetLiveStreamStatistics
    {
        private const string API_KEY = "YOUR API KEY";
        private const string ENCODING_ID = "LIVE-ENCODING-ID";

        [TestMethod]
        public void GetStatistics()
        {
            var bitmovin = new BitmovinApi(API_KEY);
            var liveStatistics = bitmovin.Statistic.LiveStatistic.GetLiveStatistics(ENCODING_ID);
            Console.WriteLine("Status: {0}", liveStatistics.Status);
            Console.WriteLine("Events: {0}", JsonConvert.SerializeObject(liveStatistics.Events, Formatting.Indented));
            Console.WriteLine("Statistics: {0}", JsonConvert.SerializeObject(liveStatistics.Statistics, Formatting.Indented));
        }


    }
}