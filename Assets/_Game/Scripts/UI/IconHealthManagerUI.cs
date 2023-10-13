using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHealthManagerUI : MonoBehaviour {
    public Action OnOverHealth;
    
    [SerializeField] private IconHealth iconHealths;
    [SerializeField] private Transform content;

    [SerializeField] private Sprite sprHealth;
    [SerializeField] private Sprite sprLoseHealth;
    
    
    private List<IconHealth> imgIconHealths;
    
    public void Init(int maxHealth) {
        if (imgIconHealths == null) {
            imgIconHealths = new List<IconHealth>();
        }

        if (maxHealth > imgIconHealths.Count) {
            int times = maxHealth - imgIconHealths.Count;
            for (int i = 0; i < times; i++) {
                IconHealth icon = Instantiate(iconHealths, content);
                icon.gameObject.SetActive(true);
                imgIconHealths.Add(icon);
            }
        }

        for (int i = 0; i < imgIconHealths.Count; i++) {
            imgIconHealths[i].ChangeSprite(sprHealth);
        }

        index = 0;
        iconHealths.gameObject.SetActive(false);
    }

    private int index;
    public void LoseHealth() {
        index++;
        if (index > imgIconHealths.Count) {
            return;
        }
        
        for (int i = 0; i < index; i++) {
            imgIconHealths[i].ChangeSprite(sprLoseHealth);
        }
        
        if (index >= imgIconHealths.Count) {
            OnOverHealth?.Invoke();
        }
    }

}
