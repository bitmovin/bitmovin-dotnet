namespace com.bitmovin.Api.Encoding
{
    public class Ts : Muxing
    {
        public double? SegmentLength { get; set; }

        public string SegmentNaming { get; set; }
    }
}