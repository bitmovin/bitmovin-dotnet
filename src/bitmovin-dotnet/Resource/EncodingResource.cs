using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Encoding;
using com.bitmovin.Api.Rest;

namespace com.bitmovin.Api.Resource
{
    public class EncodingResource<T> : AbstractResource<T>
    {
        public StreamResource<Stream> Stream { get; private set; }

        public AbstractOneEmbeddedResource<Fmp4> Fmp4 { get; private set; }
        public AbstractOneEmbeddedResource<Ts> Ts { get; private set; }
        public AbstractOneEmbeddedResource<Mp4> Mp4 { get; private set; }

        public EncodingResource(RestClient client, string url) : base(client, url)
        {
            this.Stream = new StreamResource<Stream>(client, ApiUrls.Streams);
            this.Fmp4 = new AbstractOneEmbeddedResource<Fmp4>(client, ApiUrls.FMP4Muxings);
            this.Ts = new AbstractOneEmbeddedResource<Ts>(client, ApiUrls.TSMuxings);
            this.Mp4 = new AbstractOneEmbeddedResource<Mp4>(client, ApiUrls.MP4Muxings);
        }

        public T RetrieveDetails(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return _restClient.Get<T>(retrieveUrl);
        }

        public LiveEncoding RetrieveLiveDetails(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/live", _url, id);
            return _restClient.Get<LiveEncoding>(retrieveUrl);
        }

        public string Start(string id, StartEncodingRequest s)
        {
            var postUrl = string.Format("{0}/{1}/start", _url, id);
            return _restClient.PostAndGetId<StartEncodingRequest>(postUrl, s);
        }

        public string Stop(string id)
        {
            var postUrl = string.Format("{0}/{1}/stop", _url, id);
            return _restClient.PostAndGetId(postUrl);
        }

        public string StartLive(string id, StartLiveEncodingRequest s)
        {
            var postUrl = string.Format("{0}/{1}/live/start", _url, id);
            return _restClient.PostAndGetId(postUrl, s);
        }

        public string StopLive(string id)
        {
            var postUrl = string.Format("{0}/{1}/live/stop", _url, id);
            return _restClient.PostAndGetId(postUrl);
        }

        public Task RetrieveStatus(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/status", _url, id);
            return _restClient.Get<Task>(retrieveUrl);
        }
    }
}