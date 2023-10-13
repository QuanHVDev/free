using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MessageManagerUI : PoolingManagerBase<MessageManagerUI, MessageLine> {
    public void Init(List<MapManager.ElementMessage> messagesForHint) {
        for (int i = 0; i < messagesForHint.Count; i++) {
            MessageLine messageLine = GetObjectPooledAvailable();
            messageLine.Init(messagesForHint[i]);
            messageLine.Show();
        }
    }

    public void CheckCorrect(PeopleSO peopleSO) {
        foreach (var messageLine in pooledObjects) {
            if (messageLine.gameObject.activeSelf && messageLine.RemovePeople(peopleSO)) {
                messageLine.Hide();
            }
        }
    }

    public void HideAllLine() {
        foreach (var messageLine in pooledObjects) {
            messageLine.gameObject.SetActive(false);
        }
    }
}
