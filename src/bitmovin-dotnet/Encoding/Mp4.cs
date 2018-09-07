namespace com.bitmovin.Api.Encoding
{
    public class Mp4 : Muxing
    {
        public string Filename { get; set; }

        public int? FragmentDuration { get; set; }
    }
}