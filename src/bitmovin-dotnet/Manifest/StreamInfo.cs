namespace com.bitmovin.Api.Manifest
{
    public class StreamInfo : BaseObject
    {
        public string Audio { get; set; }

        public string Video { get; set; }

        public string Subtitles { get; set; }

        public string ClosedCaptions { get; set; }

        public string SegmentPath { get; set; }

        public string Uri { get; set; }

        public string EncodingId { get; set; }

        public string StreamID { get; set; }

        public string MuxingID { get; set; }

        public string DrmID { get; set; }

        public int? StartSegmentNumber { get; set; }

        public int? EndSegmentNumber { get; set; }
    }
}