using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronPirate.Api {
    public partial class Bootstrap {
        partial void PreloadIAP() {
            IronPirate.IAP.Instance.Preload();
        }
    }    
}
