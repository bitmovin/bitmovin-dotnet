namespace com.bitmovin.Api.Codec
{
    public class VideoConfiguration : CodecConfig
    {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public long? Bitrate { get; set; }
        public float? Rate { get; set; }
    }
}