using System;

namespace com.bitmovin.Api.Exception
{
    public class BitmovinWebException
    {
        public string RequestId { get; set; }
        public string Status { get; set; }
        public BitmovinWebExceptionData Data { get; set; }

        public override string ToString()
        {
            return String.Format("RequestId: {0}, Status: {1}, Code: {2}, Message: {3}, Developer Message: {4}",
                RequestId, Status, Data.Code, Data.Message, Data.DeveloperMessage);
        }
    }
}