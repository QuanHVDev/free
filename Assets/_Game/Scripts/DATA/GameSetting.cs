using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="GameSettings", menuName ="GAME/GameSettings")]
public class GameSettings : ScriptableObject {
        
#if UNITY_EDITOR
        [UnityEditor.MenuItem("GAME/GameSettings")]
        private static void OpenGameSettings() {
            UnityEditor.Selection.activeObject = GameSettings.Ins;
        }
#endif

    private static GameSettings _ins;
    public static GameSettings Ins {
        get {
            if (_ins != null) return _ins;
            _ins = Resources.Load<GameSettings>("GameSettings");
            if (_ins == null) Debug.LogError("Cannot found GameSettings in Resources folder");
            return _ins;
        }
    }

    public int diamondPassLevel = 10;
    
    public CatsDataSO CatsDataSO;
    public TownLevelsSO TownsDataSO;

    public PeopleSO GetCatSO(string id)
    {
        foreach (var data in CatsDataSO.ListData)
        {
            if (data.id == id)
            {
                return data;
            }
        }

        Debug.Log($"NULL Cat SO with {id}");
        return null;
    }
}
