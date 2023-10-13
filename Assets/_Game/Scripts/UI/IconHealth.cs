using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHealth : MonoBehaviour {
    [SerializeField] private Image img;

    public void ChangeSprite(Sprite spr) {
        img.sprite = spr;
    }
}
