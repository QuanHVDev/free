using System;
using UnityEngine;

#if IAP
using Unity.Services.Core;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing.Extension;
#endif

namespace IronPirate.Api.IAP {
#if IAP
    public abstract class BaseIap<T> : SingletonBehaviourDontDestroy<T>, IDetailedStoreListener where T : MonoBehaviour {
#else
    public abstract class BaseIap<T> : SingletonBehaviourDontDestroy<T> where T : MonoBehaviour {
#endif
        public static bool IsPurchasing { get; private set; }
        public bool isRequesting;
        public Action onBuyFailed;
        public Action onBuyCompleted;
        public event Action<string> OnPurchasingComplete;
        private const char DefaultSymbol = '$';
        private const string DefaultIsoCurrencyCode = "USD";

        public class Meta {
            public readonly string isoCurrencyCode;
            public readonly string localizedPriceString;
            public readonly Decimal localizedPrice;
            public readonly char symbol;

            public Meta(decimal localizedPrice, char symbol, string isoCurrencyCode) {
                this.isoCurrencyCode = isoCurrencyCode;
                this.localizedPrice = localizedPrice;
                if (!string.IsNullOrEmpty(isoCurrencyCode)) {
                    localizedPriceString = string.Format("{0:0.##}{1}", this.localizedPrice, isoCurrencyCode);
                }

                this.symbol = symbol;
            }

            public Meta(decimal localizedPrice, string localizedPriceString, string isoCurrencyCode) {
                this.isoCurrencyCode = isoCurrencyCode;
                this.localizedPriceString = localizedPriceString;
                this.localizedPrice = localizedPrice;

                if (string.IsNullOrEmpty(this.localizedPriceString)) {
                    symbol = DefaultSymbol;
                }
                else {
                    if (!char.IsDigit(this.localizedPriceString[0])) {
                        symbol = this.localizedPriceString[0];
                    }
                    else if (!char.IsDigit(this.localizedPriceString[this.localizedPriceString.Length - 1])) {
                        symbol = this.localizedPriceString[this.localizedPriceString.Length - 1];
                    }
                    else {
                        symbol = DefaultSymbol;
                    }
                }
            }
        }

        private static readonly Meta emptyMeta = new Meta(0, DefaultSymbol, DefaultIsoCurrencyCode);
#if IAP
        private IStoreController storeController;
        private IExtensionProvider storeExtensionProvider;
#endif

        public void RegisterOnPurchasingComplete(Action<string> onPurchasingComplete) {
            OnPurchasingComplete += onPurchasingComplete;
        }

        public void UnRegisterOnPurchasingComplete(Action<string> onPurchasingComplete) {
            OnPurchasingComplete -= onPurchasingComplete;
        }

        public virtual void OnPurchaseStart() { }

        public virtual void OnPurchaseEnd() { }

        protected override void OnAwake() {
#if IAP
            StandardPurchasingModule module = StandardPurchasingModule.Instance();
#if UNITY_EDITOR
            module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
#endif
            var builder = ConfigurationBuilder.Instance(module);
            IapInit(builder);
            UnityPurchasing.Initialize(this, builder);
            UnityServices.InitializeAsync();
            Debug.Log("[IAP] Start Initialize");
#endif
        }
#if IAP
        protected abstract void IapInit(ConfigurationBuilder builder);


        private bool IsInitialized() {
            var isInitialized = storeController != null && storeExtensionProvider != null;
            if (!isInitialized) {
                Debug.LogErrorFormat("[IAP] Not initialized.");
            }

            return isInitialized;
        }


        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            Debug.Log("[IAP]  OnInitialized");
            storeController = controller;
            storeExtensionProvider = extensions;
            OnInitSuccessCallback();
        }

        protected abstract void OnInitSuccessCallback();

        public void OnInitializeFailed(InitializationFailureReason error) {
            Debug.LogErrorFormat("[IAP] OnInitializeFailed error: {0}", error);
            isRequesting = false;
        }

        public void OnInitializeFailed(InitializationFailureReason error, string msg) {
            Debug.LogErrorFormat("[IAP] OnInitializeFailed error: {0}", error);
            isRequesting = false;
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
            var validPurchase = true;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX

            var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

            try {
                var result = validator.Validate(args.purchasedProduct.receipt);
                foreach (IPurchaseReceipt productReceipt in result) {
                    Debug.LogFormat("productID:={0}, purchaseDate={1}, transactionID={2}", productReceipt.productID, productReceipt.purchaseDate, productReceipt.transactionID);
                }
            }
            catch (IAPSecurityException) {
#if !UNITY_EDITOR
                Debug.LogError("Invalid receipt, not unlocking content");
                validPurchase = false;
#endif
            }
#else
            Debug.LogError("Please ask store admin for google public key of iap purchase");
#endif

            if (validPurchase) {
                string id = args.purchasedProduct.definition.id;
                var meta = args.purchasedProduct.metadata;
                if (OnPurchasingComplete != null) {
                    OnPurchasingComplete.Invoke(id);
                }

                if (onBuyCompleted != null) {
                    onBuyCompleted.Invoke();
                }

                Tracking.Instance.LogPurchase(id, meta.isoCurrencyCode, (double)meta.localizedPrice);
            }

            isRequesting = false;
            IsPurchasing = false;
            OnPurchaseEnd();
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
            Debug.LogError($"[IAP] PurchaseFailed reason={failureReason}");
            if (!isRequesting) return;
            isRequesting = false;

            if (onBuyFailed != null) {
                onBuyFailed.Invoke();
                onBuyFailed = null;
            }
            IsPurchasing = false;
            OnPurchaseEnd();
        }
        
        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) {
            Debug.LogError($"[IAP] PurchaseFailed productId={failureDescription.productId}, reason={failureDescription.reason}, message={failureDescription.message}");
            if (!isRequesting) return;
            isRequesting = false;

            if (onBuyFailed != null) {
                onBuyFailed.Invoke();
                onBuyFailed = null;
            }
            IsPurchasing = false;
            OnPurchaseEnd();
        }
#endif
        public Meta GetLocalPrice(string id, Decimal defaultPrice = 0, string defaultSymbol = "$", string defaultCurencyCode = DefaultIsoCurrencyCode) {
#if IAP
            if (storeController != null) {
                var productMetadata = storeController.products.WithID(id).metadata;
                return new Meta(productMetadata.localizedPrice, productMetadata.localizedPriceString,
                    productMetadata.isoCurrencyCode);
            }
            else if (defaultPrice > 0) {
                return new Meta(defaultPrice, defaultSymbol, defaultCurencyCode);
            }

            return emptyMeta;
#else
#if UNITY_EDITOR
            Debug.Log("IAP is disable. Please add flag 'IAP' at PlayerSetting.");
#endif
            if (defaultPrice > 0) {
                return new Meta(defaultPrice, defaultSymbol, defaultCurencyCode);
            }

            return emptyMeta;
#endif
        }

        public void RestorePurchases(System.Action success = null) {
#if IAP
            if (IsInitialized()) {
                if (Application.platform == RuntimePlatform.Android) {
                    if (success != null) success.Invoke();
                }
                else if (Application.platform == RuntimePlatform.IPhonePlayer ||
                    Application.platform == RuntimePlatform.OSXPlayer) {
                    Debug.Log("RestorePurchases started ...");

                    var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();

                    apple.RestoreTransactions((result, msg) => {
                        if (result && success != null) {
                            success.Invoke();
                        }
                        Debug.LogFormat(
                            "RestorePurchases continuing: {0}. If no further messages, no purchases available to restore.",
                            result);
                    });
                }
                else {
                    Debug.LogFormat("RestorePurchases FAIL. Not supported on this platform. Current = {0}", Application.platform);
                }

                isRequesting = false;
            }
#elif UNITY_EDITOR
            Debug.Log("IAP is disable. Please add flag 'IAP' at PlayerSetting.");
#endif
        }

        public virtual void Buy(string productId, Action onBuyCompleted = null, Action onBuyFailed = null) {
#if IAP
            if (isRequesting)
                return;
            this.onBuyCompleted = onBuyCompleted;
            this.onBuyFailed = onBuyFailed;
            if (IsInitialized()) {
                Product product = storeController.products.WithID(productId);
                if (product != null) {
                    if (product.availableToPurchase) {
                        isRequesting = true;
                        Debug.LogFormat("Purchasing product asychronously: '{0}'", product.definition.id);
                        storeController.InitiatePurchase(product);
                        OnPurchaseStart();
                        IsPurchasing = true;
                    }
                    else {
                        Debug.Log(
                            "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    }
                }
            }
#else
#if UNITY_EDITOR
            if (onBuyCompleted != null) {
                    onBuyCompleted.Invoke();
            }
#endif
            Debug.Log("IAP is disable. Please add flag 'IAP' at PlayerSetting.");
#endif
        }

        public bool IsOwned(string productID) {
#if IAP
            if (!IsInitialized()) return false;
            var product = storeController.products.WithID(productID);
            if (product == null) return false;
            return product.hasReceipt;
#else
            Debug.Log("IAP is disable. Please add flag 'IAP' at PlayerSetting.");
            return false;
#endif
        }
    }
}