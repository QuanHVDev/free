using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHomeManagerUI : MonoBehaviour {
    
    
    [SerializeField] private IconHome iconHome;
    [SerializeField] private GameObject parentHomeHor;
    [SerializeField] private Transform content;

    public void Add(MapManager mapManager, GamePlayUI gamePlayUI) {
        if (IconHomes == null) {
            IconHomes = new List<IconHome>();
        }
        
        int startIndexPeople = 0;
        
        for (int i = 0; i < mapManager.GetHomes().Count; i++) {
            var e = mapManager.GetHomes()[i];
            Transform parent = content;
            
            if (e.peoples.Count > 1) {
                parent = Instantiate(parentHomeHor, transform).transform;
                parent.position = Camera.main.WorldToScreenPoint(e.home.transform.position);
                parent.gameObject.SetActive(true);
            }

            List<IconHome> targets = new List<IconHome>();
            for (int j = startIndexPeople; j < startIndexPeople + e.peoples.Count; j++) {
                IconHome icon;
                if (j >= IconHomes.Count) {
                    icon = Instantiate(iconHome, parent);
                    IconHomes.Add(icon);
                }
                else {
                    icon = IconHomes[j];
                }
                
                icon.transform.position = Camera.main.WorldToScreenPoint(e.home.transform.position);
                icon.gameObject.SetActive(true);
                targets.Add(icon);
            }

            gamePlayUI.GetIconPeopleManagerUI().Add(targets, e.peoples, startIndexPeople);
            startIndexPeople += e.peoples.Count;
        }
        
        iconHome.gameObject.SetActive(false);
        parentHomeHor.SetActive(false);
    }

    public void FinishMap() {
        // for (int i = 0; i < IconHomes.Count; i++) {
        //     Destroy(IconHomes[i].gameObject);
        // }
    }
    
    public List<IconHome> IconHomes { get; private set; }
}
