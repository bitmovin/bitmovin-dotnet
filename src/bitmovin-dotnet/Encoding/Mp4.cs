namespace com.bitmovin.Api.Encoding
{
    public class Mp4 : Muxing
    {
        public string FileName { get; set; }

        public int? FragmentDuration { get; set; }
    }
}