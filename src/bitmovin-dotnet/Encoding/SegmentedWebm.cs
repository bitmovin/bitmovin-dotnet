namespace com.bitmovin.Api.Encoding
{
    public class SegmentedWebm : Muxing
    {
        public double? SegmentLength { get; set; }

        public string SegmentNaming { get; set; }

        public string InitSegmentName { get; set; }
    }
}