using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronPirate.Api {
    public partial class Bootstrap {
        partial void PreloadCloudMessaging() {
            CloudMessaging.Instance.Preload();
        }
    }
}