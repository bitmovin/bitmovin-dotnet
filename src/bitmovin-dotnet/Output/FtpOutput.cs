using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Output
{
    public class FtpOutput : BaseOutput
    {
        public string Host { get; set; }

        public int? Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool? Passive { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FtpTransferVersion? TransferVersion { get; set; }

        public int? MaxConcurrentConnections { get; set; }
    }
}