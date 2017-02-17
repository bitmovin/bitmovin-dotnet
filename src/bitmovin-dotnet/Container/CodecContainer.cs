using com.bitmovin.Api.Codec;
using com.bitmovin.Api.Constants;
using com.bitmovin.Api.Resource;

namespace com.bitmovin.Api.Container
{
    public class CodecContainer
    {
        public CodecResource<CodecConfig> Codec { get; private set; }
        public AbstractResource<H264VideoConfiguration> H264 { get; private set; }
        public AbstractResource<H265VideoConfiguration> H265 { get; private set; }
        public AbstractResource<AACAudioConfiguration> Aac { get; private set; }

        public CodecContainer(RestClient client)
        {
            this.Codec = new CodecResource<CodecConfig>(client, ApiUrls.CodecConfig);
            this.H264 = new AbstractResource<H264VideoConfiguration>(client, ApiUrls.CodecConfigH264);
            this.H265 = new AbstractResource<H265VideoConfiguration>(client, ApiUrls.CodecConfigH265);
            this.Aac = new AbstractResource<AACAudioConfiguration>(client, ApiUrls.CodecConfigAac);
        }
    }
}