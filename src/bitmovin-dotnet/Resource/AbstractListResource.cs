using System.Collections.Generic;

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