using com.bitmovin.Api.Manifest;
using com.bitmovin.Api.Rest;

namespace com.bitmovin.Api.Resource
{
    public class HlsResource<T> : AbstractResource<T>
    {
        public HlsResource(RestClient client, string url) : base(client, url)
        {
        }

        public StreamInfo AddStreamInfo(string id, StreamInfo s)
        {
            var postUrl = string.Format("{0}/{1}/streams", _url, id);
            return _restClient.Post<StreamInfo>(postUrl, s);
        }

        public MediaInfo AddMediaInfo(string id, MediaInfo s)
        {
            var postUrl = string.Format("{0}/{1}/media", _url, id);
            return _restClient.Post<MediaInfo>(postUrl, s);
        }

        public string Start(string id)
        {
            var postUrl = string.Format("{0}/{1}/start", _url, id);
            return _restClient.PostAndGetId(postUrl);
        }

        public string Stop(string id)
        {
            var postUrl = string.Format("{0}/{1}/stop", _url, id);
            return _restClient.PostAndGetId(postUrl);
        }

        public Task RetrieveStatus(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/status", _url, id);
            return _restClient.Get<Task>(retrieveUrl);
        }
    }
}