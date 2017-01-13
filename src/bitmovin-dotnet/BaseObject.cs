using System.Collections.Generic;

namespace com.bitmovin.Api
{
    public class BaseObject
    {
        public string Id { get; set; }
        public Dictionary<string, object> CustomData { get; set; }
    }
}