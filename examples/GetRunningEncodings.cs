using System;
using com.bitmovin.Api.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.bitmovin.Api.Examples
{

    [TestClass]
    public class GetRunningEncodings
    {

        private const string API_KEY = "YOUR API KEY";

        [TestMethod]
        public void GetEncodings()
        {
            var bitmovin = new BitmovinApi(API_KEY);
            var runningEncodings = bitmovin.Encoding.Encoding.RetrieveListWithStatus(Status.RUNNING);
            foreach(var runningEncoding in runningEncodings)
            {
                Console.WriteLine("Encoding ID: {0}", runningEncoding.Id);
                Console.WriteLine("Encoding Name: {0}", runningEncoding.Name);
                Console.WriteLine("");
            }
        }
        
    }
}