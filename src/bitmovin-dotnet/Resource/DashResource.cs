using System.Threading.Tasks;
using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Manifest;
using Task = com.bitmovin.Api.Rest.Task;

namespace com.bitmovin.Api.Resource
{
    public class DashResource<T> : AbstractResource<T>
    {
        public PeriodResource<Period> Period;
        public AbstractTwoEmbeddedResource<AudioAdaptationSet> AudioAdaptationSet;
        public AbstractTwoEmbeddedResource<VideoAdaptationSet> VideoAdaptationSet;
        public AbstractTwoEmbeddedResource<AudioAdaptationSet> SubtitleAdaptationSet;
        public AbstractTwoEmbeddedResource<VideoAdaptationSet> CustomAdaptationSet;
        public AbstractThreeEmbeddedResource<Fmp4> Fmp4;
        public AbstractThreeEmbeddedResource<Webm> Webm;

        public DashResource(RestClient client, string url) : base(client, url)
        {
            this.Period = new PeriodResource<Period>(client, ApiUrls.ManifestDashAddPeriod);

            this.AudioAdaptationSet =
                new AbstractTwoEmbeddedResource<AudioAdaptationSet>(client, ApiUrls.ManifestDashAddAudioAdaptionSet);
            this.VideoAdaptationSet =
                new AbstractTwoEmbeddedResource<VideoAdaptationSet>(client, ApiUrls.ManifestDashAddVideoAdaptionSet);
            this.SubtitleAdaptationSet =
                new AbstractTwoEmbeddedResource<AudioAdaptationSet>(client, ApiUrls.ManifestDashAddSubtitleAdaptionSet);
            this.CustomAdaptationSet =
                new AbstractTwoEmbeddedResource<VideoAdaptationSet>(client, ApiUrls.ManifestDashAddCustomAdaptionSet);

            this.Fmp4 = new AbstractThreeEmbeddedResource<Fmp4>(client, ApiUrls.ManifestDashAddRepresentationFmp4);
            this.Webm = new AbstractThreeEmbeddedResource<Webm>(client, ApiUrls.ManifestDashAddRepresentationWebm);

        }

#if !NET_40

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