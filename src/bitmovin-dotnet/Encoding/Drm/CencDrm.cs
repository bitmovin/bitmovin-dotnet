using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.bitmovin.Api.Encoding.Drm
{
    public class CencDrm : BaseObject
    {
        public string CreatedAt { get; set; }

        public string ModifiedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Output> Outputs { get; set; }

        public string Key { get; set; }

        public string Kid { get; set; }

        public CencWidevine Widevine { get; set; }

        public CencPlayReady PlayReady { get; set; }
        
        public CencMarlin Marlin { get; set; }

    }
}
