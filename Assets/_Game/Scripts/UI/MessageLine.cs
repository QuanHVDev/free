using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MessageLine : MonoBehaviour {
    [SerializeField] private TMP_Text tmp;

    public List<PeopleSO> peopleSO { get; private set; }
    private Color colorToCorrect;
    public bool isFading { get; private set; }
    public void Hide() {
        isFading = true;
        tmp.DOColor(colorToCorrect, 0.7f).OnComplete(() =>
        {
            tmp.DOFade(0, 0.7f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                isFading = false;
            });
        });

    }

    public void Show() {
        tmp.color = Color.black;
        gameObject.SetActive(true);
    }

    public void Init(MapManager.ElementMessage elementMessage, Color colorToCorrect) {
        tmp.text = "- " + elementMessage.message;
        this.colorToCorrect = colorToCorrect;
        this.peopleSO = elementMessage.peoples;
    }

    public bool RemovePeople(PeopleSO peopleSo) {
        peopleSO.Remove(peopleSo);
        if (peopleSO.Count == 0) return true;
        return false;
    }
}
