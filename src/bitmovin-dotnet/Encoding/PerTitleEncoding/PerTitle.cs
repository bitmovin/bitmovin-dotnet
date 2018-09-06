namespace com.bitmovin.Api.Encoding.PerTitleEncoding
{
    public class PerTitle
    {
        public H264PerTitleConfiguration h264Configuration { get; set; }

        public PerTitle() { }

        public PerTitle(H264PerTitleConfiguration configuration)
        {
            h264Configuration = configuration;
        }
    }
}
