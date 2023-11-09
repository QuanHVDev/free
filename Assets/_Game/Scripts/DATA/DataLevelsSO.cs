using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GAME/Levels", fileName = "LevelsSO")]
public class DataLevelsSO : ScriptableObject {
    public List<MapManager> MapManagers;

    public int CountLevel() {
        

        return MapManagers.Count;
    }

    public int CountSteps()
    {
        int count = 0;
        foreach (var mapManager in MapManagers) {
            count += mapManager.GetCountMaps();
        }
        return count;
    }
}
