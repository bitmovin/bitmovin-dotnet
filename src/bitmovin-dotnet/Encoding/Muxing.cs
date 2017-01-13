using System.Collections.Generic;

namespace com.bitmovin.Api.Encoding
{
    public class Muxing : BaseObject
    {
        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<MuxingStream> Streams { get; set; }

        public List<Output> Outputs { get; set; }
    }
}