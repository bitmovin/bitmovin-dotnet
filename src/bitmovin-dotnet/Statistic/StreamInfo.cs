namespace com.bitmovin.Api.Statistic
{
    public class StreamInfo
    {
        public string CreatedAt { get; set; }
        public string StreamId { get; set; }
        public string MediaType { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public double? Rate { get; set; }
        public string Codec { get; set; }
        public double? SamplesReadPerSecondMin { get; set; }
        public double? SamplesReadPerSecondAvg { get; set; }
        public double? SamplesReadPerSecondMax { get; set; }
        public double? SamplesBackupPerSecondMin { get; set; }
        public double? SamplesBackupPerSecondAvg { get; set; }
        public double? SamplesBackupPerSecondMax { get; set; }
        public double? BytesReadPerSecondMin { get; set; }
        public double? BytesReadPerSecondAvg { get; set; }
        public double? BytesReadPerSecondMax { get; set; }
        public double? BytesBackupPerSecondMin { get; set; }
        public double? BytesBackupPerSecondAvg { get; set; }
        public double? BytesBackupPerSecondMax { get; set; }
    }
}
