
#if FIREBASE && REMOTE
using IronPirate.Api.IPFirebase;
#endif

using System;
using System.Collections.Generic;

namespace IronPirate.Api.IPFirebase {
    public struct ParseRemoteData {
        private bool booleanValue;
        private double doubleValue;
        private long longValue;
        private string stringValue;

        public bool BooleanValue => booleanValue;
        public double DoubleValue => doubleValue;
        public long LongValue => longValue;
        public string StringValue => stringValue;

        public ParseRemoteData(string value) {
            this.stringValue = value;
            this.booleanValue = false;
            this.doubleValue = 0;
            this.longValue = 0;

            bool.TryParse(value, out booleanValue);
            double.TryParse(value, out doubleValue);
            long.TryParse(value, out longValue);
        }
    }
}

namespace IronPirate {
    public class RemoteConfig : SingletonBehaviourDontDestroy<RemoteConfig> {
        private bool available;
        private event System.Action callOnAvailable;

        public Dictionary<string, object> Defaults { get; private set; } = new Dictionary<string, object>();

        protected override void OnAwake() {
            available = false;
#if FIREBASE && REMOTE
            IPFirebaseCore.Instance.InitModule(InitRemoteConfig);
#else
            DebugLog("Flag FIREBASE & REMOTE was not set for Remote Config");
#endif
        }

        private void InitRemoteConfig() {
#if FIREBASE && REMOTE
            if (available) return;
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync().ContinueWith(fetchTask => {
                if (fetchTask.IsCompleted) {
                    DebugLog("Fetch Completed.");
                    SetDefault();
                }
                else {
                    DebugLog($"Fetch faild err={fetchTask.Exception}");
                }

                available = true;
                CallOnAvailable(callOnAvailable);
            });
#endif
        }

        private void CallOnAvailable(Action task) {
            if (available) Excutor.Schedule(task);
            else callOnAvailable += task;
        }

        private void DebugLog(string message, bool error = false) {
            if (!error) {
                UnityEngine.Debug.Log($"[FIREBASE-Remote] {message}");
            }
            else {
                UnityEngine.Debug.LogError($"[FIREBASE-Remote] {message}");
            }
        }

        public void AddDefault(string key, object values) {
            if (!Defaults.ContainsKey(key)) {
                Defaults.Add(key, values);
                DebugLog($"Add Default {key}={values}");
                if (available) SetDefault();
            }
        }

        private void SetDefault() {
            DebugLog("TODO FIREBASE REMOVE CONFIG");
            //Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(Defaults);
        }

#if FIREBASE && REMOTE
        private ParseRemoteData GetConfigValue(string key, string configNamespace) {
            var configValue = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(key);
            if (string.IsNullOrEmpty(configNamespace)) {
                DebugLog($"Get remote key ={key}, value=[{configValue.StringValue}]");
            }
            else {
                DebugLog($"Get remote key={key}, namespace={configNamespace}, value=[{configValue.StringValue}]");
            }

            return new ParseRemoteData(configValue.StringValue);
        }
#endif

        #region Get Internal Value
        private string GetInternalDefaultString(string key) {
            object value;
            if (Defaults.TryGetValue(key, out value)) {
                return value.ToString();
            }

            return String.Empty;
        }

        private bool GetInternalDefaultBool(string key) {
            object value;
            if (Defaults.TryGetValue(key, out value)) {
                if (value is bool) {
                    return (bool)value;
                }
            }

            return false;
        }

        private long GetInternalDefaultLong(string key) {
            object value;
            if (Defaults.TryGetValue(key, out value)) {
                if (value is long) {
                    return (long)value;
                }

                if (value is int) {
                    return (int)value;
                }
            }

            return 0;
        }

        private double GetInternalDefaultDouble(string key) {
            object value;
            if (Defaults.TryGetValue(key, out value)) {
                if (value is float) {
                    return (float)value;
                }

                if (value is double) {
                    return (double)value;
                }
            }

            return 0;
        }
        #endregion

        #region Public Get Value
        /// <summary>
        /// This method will return server value if server fetch success. else return default value.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public string GetString(string key, string configNamespace = null) {
#if FIREBASE && REMOTE
            if (available) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.StringValue;
            }
#endif
            var value = GetInternalDefaultString(key);
            DebugLog($"Get default key={key}, value=[{value}]");
            return value;
        }

        /// <summary>
        /// This method will return server value if server fetch success. else return default value.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public bool GetBool(string key, string configNamespace = null) {
#if FIREBASE && REMOTE
            if (available) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.BooleanValue;
            }
#endif
            var value = GetInternalDefaultBool(key);
            DebugLog($"Get default key={key}, value=[{value}]");
            return value;
        }

        /// <summary>
        /// This method will return server value if server fetch success. else return default value.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public long GetLong(string key, string configNamespace = null) {
#if FIREBASE && REMOTE
            if (available) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.LongValue;
            }
#endif
            var value = GetInternalDefaultLong(key);
            DebugLog($"Get default key={key}, value=[{value}]");
            return value;
        }

        /// <summary>
        /// This method will return server value if server fetch success. else return default value.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public double GetDouble(string key, string configNamespace = null) {
#if FIREBASE && REMOTE
            if (available) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.DoubleValue;
            }
#endif
            var value = Instance.GetInternalDefaultDouble(key);
            DebugLog($"Get default key={key}, value=[{value}]");
            return value;
        }

        /// <summary>
        /// This method will wait till the server is ready to fetch. You should use a time out countdown to control your logic cause of this callback can be never call.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="action">the callback when fetch completed</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public void GetStringAsync(string key, Action<string> action, string configNamespace = null) {
            CallOnAvailable(() => {
                action.Invoke(GetString(key, configNamespace));
            });
        }

        /// <summary>
        /// This method will wait till the server is ready to fetch. You should use a time out countdown to control your logic cause of this callback can be never call.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="action">the callback when fetch completed</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public void GetLongAsync(string key, Action<long> action, string configNamespace = null) {
            CallOnAvailable(() => {
                action.Invoke(GetLong(key, configNamespace));
            });
        }

        /// <summary>
        /// This method will wait till the server is ready to fetch. You should use a time out countdown to control your logic cause of this callback can be never call.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="action">the callback when fetch completed</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public void GetBoolAsync(string key, Action<bool> action, string configNamespace = null) {
            CallOnAvailable(() => {
                action.Invoke(GetBool(key, configNamespace));
            });
        }

        /// <summary>
        /// This method will wait till the server is ready to fetch. You should use a time out countdown to control your logic cause of this callback can be never call.
        /// <para>If the key not exist, return default. You can use 'AddDefault' to set custom default value.</para>
        /// </summary>
        /// <param name="key">key to get value</param>
        /// <param name="action">the callback when fetch completed</param>
        /// <param name="configNamespace">the parent folder of the key</param>
        /// <returns></returns>
        public void GetDoubleAsync(string key, Action<double> action, string configNamespace = null) {
            CallOnAvailable(() => {
                action.Invoke(GetDouble(key, configNamespace));
            });
        }
        #endregion

    }
}