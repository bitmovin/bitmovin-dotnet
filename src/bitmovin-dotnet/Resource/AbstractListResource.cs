using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.bitmovin.Api.Resource
{
    public class AbstractListResource<T>
    {
        protected RestClient _restClient;

        protected string _url;

        public AbstractListResource(RestClient client, string url)
        {
            this._restClient = client;
            this._url = url;
        }

#if !NET_40

        public async Task<List<T>> RetrieveListAsync(int offset, int limit)
        {
            var retrieveUrl = string.Format("{0}?offset={1}&limit={2}", _url, offset, limit);
            return await _restClient.GetListAsync<T>(retrieveUrl);
        }

        public async Task<List<T>> RetrieveAllIterativeAsync(int offset, int limit)
        {
            var retrieveUrl = string.Format("{0}?offset={1}&limit={2}", _url, offset, limit);
            return await _restClient.GetAllIterativeAsync<T>(retrieveUrl);
        }

#endif

        public List<T> RetrieveList(int offset, int limit)
        {
            var retrieveUrl = string.Format("{0}?offset={1}&limit={2}", _url, offset, limit);
            return _restClient.GetList<T>(retrieveUrl);
        }

        public List<T> RetrieveAllIterative(int offset, int limit)
        {
            var retrieveUrl = string.Format("{0}?offset={1}&limit={2}", _url, offset, limit);
            return _restClient.GetAllIterative<T>(retrieveUrl);
        }
    }
}