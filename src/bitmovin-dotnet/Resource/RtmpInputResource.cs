using System.Threading.Tasks;

namespace com.bitmovin.Api.Resource
{
    public class RtmpInputResource<T> : AbstractListResource<T>
    {
        public RtmpInputResource(RestClient client, string url) : base(client, url)
        {
        }

#if !NET_40

        public async Task<T> RetrieveAsync(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return await _restClient.GetAsync<T>(retrieveUrl);
        }

#endif 

        public T Retrieve(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return _restClient.Get<T>(retrieveUrl);
        }
    }
}