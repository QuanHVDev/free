
namespace IronPirate.Api {
    public partial class Bootstrap {
        partial void PreloadRemoteConfig() {
            RemoteConfig.Instance.Preload();
        }
    }
}