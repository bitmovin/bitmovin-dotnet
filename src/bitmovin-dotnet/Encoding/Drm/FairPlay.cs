using System.Collections.Generic;
using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Encoding.Drm
{
    public class FairPlay : BaseObject
    {
        public string CreatedAt { get; set; }

        public string ModifiedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Output> Outputs { get; set; }

        public string Key { get; set; }

        public string Iv { get; set; }

        public string Uri { get; set; }

    }
}
