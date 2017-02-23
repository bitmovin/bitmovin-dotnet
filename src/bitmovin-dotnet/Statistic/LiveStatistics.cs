using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.bitmovin.Api;

namespace com.bitmovin.Api.Statistic
{
    public class LiveStatistics : BaseObject
    {

        public string CreatedAt { get; set; }
        public string EncodingId { get; set; }
        public string Status { get; set; }
        public List<LiveEvent> Events { get; set; }
        public List<LiveStatistic> Statistics { get; set; }


    }
}
