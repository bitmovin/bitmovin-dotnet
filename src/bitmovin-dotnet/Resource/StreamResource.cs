using com.bitmovin.Api.Input;

namespace com.bitmovin.Api.Resource
{
    public class StreamResource<T> : AbstractOneEmbeddedResource<T>
    {
        public StreamResource(RestClient client, string url) : base(client, url)
        {
        }

        public AnalysisDetail RetrieveInputDetails(string id, string sub_id)
        {
            var retrieveUrl = string.Format(_url + "/{1}/input", id, sub_id);
            return _restClient.Get<AnalysisDetail>(retrieveUrl);
        }
    }
}