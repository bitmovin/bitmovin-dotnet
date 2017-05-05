using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitmovin_dotnet_vs2015.Resource;
using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Encoding;
using com.bitmovin.Api.Enums;
using Task = com.bitmovin.Api.Rest.Task;

namespace com.bitmovin.Api.Resource
{
    public class EncodingResource<T> : AbstractResource<T>
    {
        public StreamResource<Stream> Stream { get; private set; }

        public Fmp4Resource<Fmp4> Fmp4 { get; private set; }
        public TsResource<Ts> Ts { get; private set; }
        public AbstractOneEmbeddedResource<Mp4> Mp4 { get; private set; }
        public AbstractOneEmbeddedResource<SegmentedWebm> SegmentedWebm { get; private set; }

        public EncodingResource(RestClient client, string url) : base(client, url)
        {
            this.Stream = new StreamResource<Stream>(client, ApiUrls.Streams);
            this.Fmp4 = new Fmp4Resource<Fmp4>(client, ApiUrls.FMP4Muxings);
            this.Ts = new TsResource<Ts>(client, ApiUrls.TSMuxings);
            this.Mp4 = new AbstractOneEmbeddedResource<Mp4>(client, ApiUrls.MP4Muxings);
            this.SegmentedWebm = new AbstractOneEmbeddedResource<SegmentedWebm>(client, ApiUrls.WebmMuxings);
        }

#if !NET_40

        public async Task<List<T>> RetrieveListWithStatusAsync(IEnumerable<Status> status, int offset = 0, int limit = 100)
        {
            var retrieveUrl = string.Format("{0}?status={1}&offset={2}&limit={3}", _url, string.Join(",", status.Select(c => c.ToString())), offset, limit);
            return await _restClient.GetListAsync<T>(retrieveUrl);
        }

        public async Task<List<T>> RetrieveListWithStatusAsync(Status status, int offset = 0, int limit = 100)
        {
            var retrieveUrl = string.Format("{0}?status={1}&offset={2}&limit={3}", _url, status, offset, limit);
            return await _restClient.GetListAsync<T>(retrieveUrl);
        }
   
        public async Task<T> RetrieveDetailsAsync(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}", _url, id);
            return await _restClient.GetAsync<T>(retrieveUrl);
        }

        public async Task<LiveEncoding> RetrieveLiveDetailsAsync(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/live", _url, id);
            return await _restClient.GetAsync<LiveEncoding>(retrieveUrl);
        }

        public async Task<string> StartAsync(string id, StartEncodingRequest s)
        {
            var postUrl = string.Format("{0}/{1}/start", _url, id);
            return await _restClient.PostAndGetIdAsync<StartEncodingRequest>(postUrl, s);
        }

        public async Task<string> StartAsync(string id)
        {
            return await StartAsync(id, new StartEncodingRequest());
        }

        public async Task<string> StopAsync(string id)
        {
            var postUrl = string.Format("{0}/{1}/stop", _url, id);
            return await _restClient.PostAndGetIdAsync(postUrl);
        }

        public async Task<string> StartLiveAsync(string id, StartLiveEncodingRequest s)
        {
            var postUrl = string.Format("{0}/{1}/live/start", _url, id);
            return await _restClient.PostAndGetIdAsync(postUrl, s);
        }

        public async Task<string> StopLiveAsync(string id)
        {
            var postUrl = string.Format("{0}/{1}/live/stop", _url, id);
            return await _restClient.PostAndGetIdAsync(postUrl);
        }

        public async Task<Task> RetrieveStatusAsync(string id)
        {
            var retrieveUrl = string.Format("{0}/{1}/status", _url, id);
            return await _restClient.GetAsync<Task>(retrieveUrl);
        }

#endif

        public List<T> RetrieveListWithStatus(IEnumerable<Status> status, int offset = 0, int limit = 100)
        {
            var retrieveUrl = string.Format("{0}?status={1}&offset={2}&limit={3}", _url, string.Join(",", status.Select(c => c.ToString())), offset, limit);
            return _restClient.GetList<T>(retrieveUrl);
        }
  
        public List<T> RetrieveListWithStatus(Status status, int offset = 0, int limit = 100)
        {
            var retrieveUrl = string.Format("{0}?status={1}&offset={2}&limit={3}", _url, status, offset, limit);
            return _restClient.GetList<T>(retrieveUrl);
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

        public string Start(string id)
        {
            return Start(id, new StartEncodingRequest());
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