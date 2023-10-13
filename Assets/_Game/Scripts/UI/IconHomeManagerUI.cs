using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHomeManagerUI : MonoBehaviour {
    
    
    [SerializeField] private GameObject iconHome;
    [SerializeField] private GameObject parentHomeHor;
    
    public void Init(MapManager mapManager, GamePlayUI gamePlayUI) {
        IconHomes = new List<Transform>();
        foreach (MapManager.ElementMap e in mapManager.GetHomes()) {
            Transform parent = transform;
            
            if (e.peoples.Count > 1) {
                parent = Instantiate(parentHomeHor, transform).transform;
                parent.position = Camera.main.WorldToScreenPoint(e.home.transform.position);
                parent.gameObject.SetActive(true);
            }

            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < e.peoples.Count; i++) {
                var icon = Instantiate(iconHome, parent);
                icon.transform.position = Camera.main.WorldToScreenPoint(e.home.transform.position);
                icon.SetActive(true);
                
                targets.Add(icon.transform);
                IconHomes.Add(icon.transform);
            }

            gamePlayUI.GetIconPeopleManagerUI().Add(targets, e.peoples);
        }
        
        iconHome.SetActive(false);
        parentHomeHor.SetActive(false);
    }

    public void FinishMap() {
        for (int i = 0; i < IconHomes.Count; i++) {
            Destroy(IconHomes[i].gameObject);
        }
    }
    
    public List<Transform> IconHomes { get; private set; }
}
