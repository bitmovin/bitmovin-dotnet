using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.bitmovin.Api.Statistic;

namespace com.bitmovin.Api.Resource
{
    public class LiveStatisticResource
    {
        protected RestClient _restClient;
        protected string _url;

        public LiveStatisticResource(RestClient client, string url)
        {
            this._restClient = client;
            this._url = url;
        }

        public LiveStatistics GetLiveStatistics(string encodingId)
        {
            var retrieveUrl = _url.Replace("{encoding_id}", encodingId);
            return _restClient.Get<LiveStatistics>(retrieveUrl);
        }

    }
}
