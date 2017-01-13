using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Resource;

namespace com.bitmovin.Api.Container
{
    public class EncodingContainer
    {
        public EncodingResource<Encoding.Encoding> Encoding { get; private set; }

        public EncodingContainer(RestClient client)
        {
            this.Encoding = new EncodingResource<Encoding.Encoding>(client, ApiUrls.Encodings);
        }
    }
}