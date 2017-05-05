using com.bitmovin.Api;
using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Encoding.Drm;
using com.bitmovin.Api.Resource;

namespace bitmovin_dotnet_vs2015.Resource
{
    public class TsResource<T> : AbstractOneEmbeddedResource<T>
    {

        public AbstractTwoEmbeddedResource<AESEncryption> Aes;
        public AbstractTwoEmbeddedResource<FairPlay> FairPlay;

        public TsResource(RestClient client, string url) : base(client, url)
        {
            Aes = new AbstractTwoEmbeddedResource<AESEncryption>(client, ApiUrls.AesEncryptionDrms);
            FairPlay = new AbstractTwoEmbeddedResource<FairPlay>(client, ApiUrls.FairPlayTsDrms);
        }

    }
}
