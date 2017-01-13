namespace com.bitmovin.Api.Resource
{
    public class CloudOutputResource<T> : AbstractListResource<T>
    {
        public CloudOutputResource(RestClient client, string url) : base(client, url)
        {
        }

        public T Retrieve(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return _restClient.Get<T>(retrieveUrl);
        }
    }
}