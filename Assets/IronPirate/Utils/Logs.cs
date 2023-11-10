using UnityEngine;

public class Logs {
    public const string DebugBuildCondition = "CUSTOM_DEBUG";
    public const string ProductionCondition = "PRODUCTION";

    public static bool IsEditor {
        get {
#if UNITY_EDITOR
            return true;
#endif
            return false;
        }
    }

    public static bool ForMarketing {
        get {
#if FOR_MKT
            return true;
#else
            return false;
#endif
        }
    }

    public static bool CheatEnable {
        get {
#if PRODUCTION
            return false;
#else
            return true;
#endif
        }
    }

    public static bool IsEnable {
        get {
#if UNITY_EDITOR || CUSTOM_DEBUG
            return true;
#else
            return false;
#endif
        }
    }

    [System.Diagnostics.Conditional(DebugBuildCondition)]
    public static void Log(string message) {
        if (!IsEnable) return;
        Debug.Log(message);
    }

    public static void LogError(string message) {
        Debug.LogError(message);
    }
}
