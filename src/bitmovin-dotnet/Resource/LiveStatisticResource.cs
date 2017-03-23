using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

#if !NET_40

        public async Task<LiveStatistics> GetLiveStatisticsAsync(string encodingId)
        {
            var retrieveUrl = _url.Replace("{encoding_id}", encodingId);
            return await _restClient.GetAsync<LiveStatistics>(retrieveUrl);
        }

#endif 

        public LiveStatistics GetLiveStatistics(string encodingId)
        {
            var retrieveUrl = _url.Replace("{encoding_id}", encodingId);
            return _restClient.Get<LiveStatistics>(retrieveUrl);
        }

    }
}
