using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MessageLine : MonoBehaviour {
    [SerializeField] private TMP_Text tmp;

    public List<PeopleSO> peopleSO { get; private set; }
    public void Hide() {
        tmp.DOFade(0, 1).OnComplete(()=> {
            gameObject.SetActive(false);
        });
    }

    public void Show() {
        tmp.DOFade(1, 0.01f).OnComplete(()=> {
            gameObject.SetActive(true);
        });
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
