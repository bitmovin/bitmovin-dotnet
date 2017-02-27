using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.bitmovin.Api.Statistic
{
    public class LiveEvent
    {
        public string Time { get; set; }
        public Dictionary<string, object> Details { get; set; }

        public LiveEvent()
        {
            Details = new Dictionary<string, object>();
        }

    }
}
