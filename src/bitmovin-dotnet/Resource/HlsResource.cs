using System.Threading.Tasks;
using com.bitmovin.Api.Manifest;
using Task = com.bitmovin.Api.Rest.Task;

namespace com.bitmovin.Api.Resource
{
    public class HlsResource<T> : AbstractResource<T>
    {
        public HlsResource(RestClient client, string url) : base(client, url)
        {
        }

#if !NET_40

        public async Task<StreamInfo> AddStreamInfoAsync(string id, StreamInfo s)
        {
            var postUrl = string.Format("{0}/{1}/streams", _url, id);
            return await _restClient.PostAsync<StreamInfo>(postUrl, s);
        }

        public async Task<MediaInfo> AddMediaInfoAsync(string id, MediaInfo s)
        {
            var postUrl = string.Format("{0}/{1}/media", _url, id);
            return await _restClient.PostAsync<MediaInfo>(postUrl, s);
        }

        public async Task<string> StartAsync(string id)
        {
            var postUrl = string.Format("{0}/{1}/start", _url, id);
            return await _restClient.PostAndGetIdAsync(postUrl);
        }

        public async Task<string> StopAsync(string id)
        {
            var postUrl = string.Format("{0}/{1}/stop", _url, id);
            return await _restClient.PostAndGetIdAsync(postUrl);
        }

        public async Task<Task> RetrieveStatusAsync(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/status", _url, id);
            return await _restClient.GetAsync<Task>(retrieveUrl);
        }

#endif 

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