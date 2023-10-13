using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {
    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private Transform spawnMap;
    
    [SerializeField] private DataLevelsSO dataLevelsSO;
    [SerializeField] private int indexMap;
    private MapManager currentMapManager;
    
    // health
    private int maxHealth = 3;
    private int currentHealth;
    
    
    private void Start() {
        SpawnLevel();
    }
    
    public void SpawnLevel() {
        if (currentMapManager) {
            gamePlayUI.GetIconHomeManagerUI().FinishMap();
            gamePlayUI.GetIconPeopleManagerUI().TryAgain();
            Destroy(currentMapManager.gameObject);
        }

        LoadLevel(indexMap);
        gamePlayUI.GetIconHomeManagerUI().Add(currentMapManager, gamePlayUI);
        gamePlayUI.GetIconPeopleManagerUI().SetAllHomesForIcon(gamePlayUI.GetIconHomeManagerUI().IconHomes);
        gamePlayUI.GetMessageManagerUI().Init(currentMapManager.GetMessagesForHint());
        
        LoadHealth();

        // var inv = UserDataController.Instance.GetData<Inventory>(UserDataKeys.USER_INVENTORY, out _);
        // if (inv.currentLevel >= data.lvls.Count) {
        //     gamePlayUI.LoadContent(inv.currentLevel);
        // }
        // else {
        //     gamePlayUI.LoadContent(currentLevel);
        // }
    }
    
    private void LoadLevel(int index) {
        currentMapManager = Instantiate(dataLevelsSO.MapManagers[index], spawnMap);
        currentMapManager.OnFinishLevel += gamePlayUI.ShowUIWin;
        currentMapManager.OnFinishLevel += () => {
            indexMap++;
            if (indexMap >= dataLevelsSO.MapManagers.Count) indexMap = 0;
        };
    }

    private void LoadHealth() {
        currentHealth = maxHealth;
        gamePlayUI.GetIconHealthManagerUI().Init(currentHealth);
    }

    public void Incorrent() {
        --currentHealth;
        gamePlayUI.GetIconHealthManagerUI().LoseHealth();
    }

    public MapManager GetCurrentMapManager() {
        return currentMapManager;
    }
}
