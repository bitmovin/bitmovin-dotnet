using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Manifest;
using com.bitmovin.Api.Resource;

namespace com.bitmovin.Api.Container
{
    public class ManifestContainer
    {
        public AbstractListResource<BaseManifest> Manifest { get; private set; }

        public DashResource<Dash> Dash { get; private set; }

        public HlsResource<Hls> Hls { get; private set; }

        public ManifestContainer(RestClient client)
        {
            this.Manifest = new AbstractListResource<BaseManifest>(client, ApiUrls.Manifests);
            this.Dash = new DashResource<Dash>(client, ApiUrls.ManifestDash);
            this.Hls = new HlsResource<Hls>(client, ApiUrls.ManifestHls);
        }
    }
}