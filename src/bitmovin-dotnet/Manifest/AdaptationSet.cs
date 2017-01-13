using System.Collections.Generic;
using com.bitmovin.Api.Enums;

namespace com.bitmovin.Api.Manifest
{
    public class AdaptationSet
    {
        public string Id { get; set; }

        public List<CustomAttribute> CustomAttributes { get; set; }

        public List<Role> Roles { get; set; }
    }
}