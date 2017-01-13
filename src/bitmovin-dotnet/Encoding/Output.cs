using System.Collections.Generic;
using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Encoding
{
    public class Acl
    {
        public string Scope { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Permission? Permission { get; set; }
    }

    public class Output
    {
        public string OutputId { get; set; }

        public string OutputPath { get; set; }

        public List<Acl> Acl { get; set; }
    }
}