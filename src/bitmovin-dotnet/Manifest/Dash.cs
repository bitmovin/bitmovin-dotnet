using System.Collections.Generic;

namespace com.bitmovin.Api.Manifest
{
    public class Dash : BaseObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Encoding.Output> Outputs { get; set; }

        public string ManifestName { get; set; }
    }
}