
using System;
using IronPirate.Api.IAP;

#if IAP
using UnityEngine.Purchasing;
#endif

namespace IronPirate {
    public class IAP : BaseIap<IAP> {
        private const string PackageName = "";//"com.weapon.running.shooting.craft.gun.";

        public const string RemoveAdsKey = PackageName + "removeads";

        public const string SpecialHandGunKey = PackageName + "handgun";
        public const string SpecialSMGKey = PackageName + "machinegun";
        public const string SpecialRifleKey = PackageName + "riflegun";
        public const string SpecialShotGunKey = PackageName + "shotgun";
        public const string SpecialSniperKey = PackageName + "snipergun";

        public const string SpecialPackKey = PackageName + "specialpack";

        public bool IsRemovedAds => IsOwned(RemoveAdsKey) || IsOwned(SpecialPackKey);

        public event Action onInitialized;
#if IAP
        protected override void IapInit(ConfigurationBuilder builder) {
            builder.AddProduct(RemoveAdsKey, ProductType.NonConsumable);
            builder.AddProduct(SpecialHandGunKey, ProductType.NonConsumable);
            builder.AddProduct(SpecialSMGKey, ProductType.NonConsumable);
            builder.AddProduct(SpecialRifleKey, ProductType.NonConsumable);
            builder.AddProduct(SpecialShotGunKey, ProductType.NonConsumable);
            builder.AddProduct(SpecialSniperKey, ProductType.NonConsumable);
            builder.AddProduct(SpecialPackKey, ProductType.NonConsumable);
        }

        protected override void OnInitSuccessCallback() {
            if (onInitialized != null) onInitialized.Invoke();
        }
#endif
    }

    public partial class GameEvents {
        public static Action onRemoveAd;        
    }
}
