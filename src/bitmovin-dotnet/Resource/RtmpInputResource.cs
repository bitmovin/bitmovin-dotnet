namespace com.bitmovin.Api.Resource
{
    public class RtmpInputResource<T> : AbstractListResource<T>
    {
        public RtmpInputResource(RestClient client, string url) : base(client, url)
        {
        }

        public T Retrieve(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return _restClient.Get<T>(retrieveUrl);
        }
    }
}