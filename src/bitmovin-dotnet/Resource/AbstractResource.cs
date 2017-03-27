using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.bitmovin.Api.Resource
{
    public class AbstractResource<T> : AbstractListResource<T>
    {
        public AbstractResource(RestClient client, string url) : base(client, url)
        {
        }

#if !NET_40

        public async Task<T> CreateAsync(T item)
        {
            return await _restClient.PostAsync<T>(_url, item);
        }

        public async Task<T> RetrieveAsync(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return await _restClient.GetAsync<T>(retrieveUrl);
        }

        public async Task<Dictionary<string, object>> RetrieveCustomDataAsync(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/customData", _url, id);
            return await _restClient.GetCustomDataAsync(retrieveUrl);
        }

        public async Task DeleteAsync(string id)
        {
            var deleteUrl = string.Format("{0}/{1}", _url, id);
            await _restClient.DeleteAsync(deleteUrl);
        }

#endif

        public T Create(T item)
        {
            return _restClient.Post<T>(_url, item);
        }

        public T Retrieve(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return _restClient.Get<T>(retrieveUrl);
        }

        public Dictionary<string, object> RetrieveCustomData(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/customData", _url, id);
            return _restClient.GetCustomData(retrieveUrl);
        }

        public void Delete(string id)
        {
            var deleteUrl = string.Format("{0}/{1}", _url, id);
            _restClient.Delete(deleteUrl);
        }
    }
}
