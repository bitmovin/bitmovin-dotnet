using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.bitmovin.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.bitmovin.Api.Encoding.Drm
{
    public class AESEncryption : BaseObject
    {
        public string CreatedAt { get; set; }

        public string ModifiedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Output> Outputs { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AesMethod Method { get; set; }

        public string Key { get; set; }

        public string Iv { get; set; }

        public string KeyFileUri { get; set; }

    }
}
