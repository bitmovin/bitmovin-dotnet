using System.Collections.Generic;

namespace com.bitmovin.Api.Resource
{
    public class AbstractResource<T> : AbstractListResource<T>
    {
        public AbstractResource(RestClient client, string url) : base(client, url)
        {
        }

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
            return;
        }
    }
}
