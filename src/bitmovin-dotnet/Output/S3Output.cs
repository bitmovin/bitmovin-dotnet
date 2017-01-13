using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Output
{
    public class S3Output : BaseOutput
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string BucketName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AwsCloudRegion? CloudRegion { get; set; }
    }
}