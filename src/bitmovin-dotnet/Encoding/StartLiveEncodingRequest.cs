using System.Collections.Generic;

namespace com.bitmovin.Api.Encoding
{
    public class StartLiveEncodingRequest
    {
        public string StreamKey { get; set; }
        public List<LiveDashManifest> DashManifests { get; set; }
        public List<LiveHlsManifest> HlsManifests { get; set; }
    }
}