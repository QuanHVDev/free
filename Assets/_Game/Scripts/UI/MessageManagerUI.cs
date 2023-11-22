using System.Collections.Generic;
using UnityEngine;

public class MessageManagerUI : PoolingManagerBase<MessageManagerUI, MessageLine>
{
    [SerializeField] private Color colorToCorrect;
    public void Init(List<MapManager.ElementMessage> messagesForHint)
    {
        HideAllLine();
        for (int i = 0; i < messagesForHint.Count; i++) {
            MessageLine messageLine = GetObjectPooledAvailable();
            messageLine.Init(messagesForHint[i], colorToCorrect);
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
