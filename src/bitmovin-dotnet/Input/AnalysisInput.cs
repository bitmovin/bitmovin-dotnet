using com.bitmovin.Api.Enums;

namespace com.bitmovin.Api.Input
{
    public class AnalysisInput : BaseObject
    {
        public string Path { get; set; }

        public CloudRegion? CloudRegion { get; set; }
    }
}