using com.bitmovin.Api;
using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Encoding.Drm;
using com.bitmovin.Api.Resource;

namespace bitmovin_dotnet_vs2015.Resource
{
    public class Fmp4Resource<T> : AbstractOneEmbeddedResource<T>
    {

        public AbstractTwoEmbeddedResource<CencDrm> CencDrm;

        public Fmp4Resource(RestClient client, string url) : base(client, url)
        {
            CencDrm = new AbstractTwoEmbeddedResource<CencDrm>(client, ApiUrls.CencDrms);
        }

    }
}
