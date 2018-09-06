namespace com.bitmovin.Api.Encoding.PerTitleEncoding
{
    public abstract class PerTitleConfiguration
    {
        public int? MinBitrate { get; set; }

        public int? MaxBitrate { get; set; }

        public double? MinBitrateStepSize { get; set; }

        public double? MaxBitrateStepSize { get; set; }

        public AutoRepresentation AutoRepresentations { get; set; }
    }
}
