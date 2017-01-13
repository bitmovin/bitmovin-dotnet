namespace com.bitmovin.Api.Encoding
{
    public class LiveDashManifest
    {
        public string ManifestId { get; set; }
        public double? Timeshift { get; set; }
        public double? LiveEdgeOffset { get; set; }
    }
}