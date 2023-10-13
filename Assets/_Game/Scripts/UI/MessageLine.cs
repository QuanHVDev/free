using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MessageLine : MonoBehaviour {
    [SerializeField] private TMP_Text tmp;

    public List<PeopleSO> peopleSO { get; private set; }
    public bool isFading { get; private set; }
    public void Hide() {
        isFading = true;
        tmp.DOFade(0, 0.5f).OnComplete(()=> {
            gameObject.SetActive(false);
            isFading = false;
        });
    }

    public void Show() {
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b);
        gameObject.SetActive(true);
    }

    public void Init(MapManager.ElementMessage elementMessage) {
        tmp.text = elementMessage.message;
        this.peopleSO = elementMessage.peoples;
    }

    public bool RemovePeople(PeopleSO peopleSo) {
        peopleSO.Remove(peopleSo);
        if (peopleSO.Count == 0) return true;
        return false;
    }
}
