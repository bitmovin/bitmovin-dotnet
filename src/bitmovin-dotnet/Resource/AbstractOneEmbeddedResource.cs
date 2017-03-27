using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.bitmovin.Api.Resource
{
    public class AbstractOneEmbeddedResource<T>
    {
        protected RestClient _restClient;

        protected string _url;

        public AbstractOneEmbeddedResource(RestClient client, string url)
        {
            this._restClient = client;
            this._url = url;
        }

#if !NET_40

        public async Task<List<T>> RetrieveListAsync(string id, int offset, int limit)
        {
            var retrieveUrl = string.Format(_url + "?offset={1}&limit={2}", id, offset, limit);
            return await _restClient.GetListAsync<T>(retrieveUrl);
        }

        public async Task<List<T>> RetrieveAllIterativeAsync(string id, int offset, int limit)
        {
            var retrieveUrl = string.Format(_url + "?offset={1}&limit={2}", id, offset, limit);
            return await _restClient.GetAllIterativeAsync<T>(retrieveUrl);
        }

        public async Task<T> CreateAsync(string id, T item)
        {
            var postUrl = string.Format(_url, id);
            return await _restClient.PostAsync(postUrl, item);
        }

        public async Task<T> RetrieveAsync(string id, string sub_id)
        {
            var retrieveUrl = string.Format(_url + "/{1}", id, sub_id);
            return await _restClient.GetAsync<T>(retrieveUrl);
        }

        public async Task<Dictionary<string, object>> RetrieveCustomDataAsync(string id, string sub_id)
        {
            var retrieveUrl = string.Format(_url + "/{1}/customData", id, sub_id);
            return await _restClient.GetCustomDataAsync(retrieveUrl);
        }

        public async Task DeleteAsync(string id, string sub_id)
        {
            var deleteUrl = string.Format(_url + "/{1}", id, sub_id);
            await _restClient.DeleteAsync(deleteUrl);
        }

#endif

        public List<T> RetrieveList(string id, int offset, int limit)
        {
            var retrieveUrl = string.Format(_url + "?offset={1}&limit={2}", id, offset, limit);
            return _restClient.GetList<T>(retrieveUrl);
        }

        public List<T> RetrieveAllIterative(string id, int offset, int limit)
        {
            var retrieveUrl = string.Format(_url + "?offset={1}&limit={2}", id, offset, limit);
            return _restClient.GetAllIterative<T>(retrieveUrl);
        }

        public T Create(string id, T item)
        {
            var postUrl = string.Format(_url, id);
            return _restClient.Post<T>(postUrl, item);
        }

        public T Retrieve(string id, string sub_id)
        {
            var retrieveUrl = string.Format(_url + "/{1}", id, sub_id);
            return _restClient.Get<T>(retrieveUrl);
        }

        public Dictionary<string, object> RetrieveCustomData(string id, string sub_id)
        {
            var retrieveUrl = string.Format(_url + "/{1}/customData", id, sub_id);
            return _restClient.GetCustomData(retrieveUrl);
        }

        public void Delete(string id, string sub_id)
        {
            var deleteUrl = string.Format(_url + "/{1}", id, sub_id);
            _restClient.Delete(deleteUrl);
        }
    }
}