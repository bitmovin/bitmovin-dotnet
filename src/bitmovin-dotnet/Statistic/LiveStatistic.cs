using System.Collections.Generic;

namespace com.bitmovin.Api.Statistic
{
    public class LiveStatistic
    {
        public string CreatedAt { get; set; }
        public string Time { get; set; }
        public List<StreamInfo> StreamInfos { get; set; }

        public LiveStatistic()
        {
            StreamInfos = new List<StreamInfo>();
        }

    }
}
