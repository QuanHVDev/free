using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MessageManagerUI : MonoBehaviour {
    [SerializeField] private MessageLine tmpMessage;
    [SerializeField] private Transform content;
    private List<MessageLine> messageLineList;
    public void Init(List<MapManager.ElementMessage> messagesForHint) {
        if (messageLineList == null) {
            messageLineList = new List<MessageLine>();
        }

        for (int i = 0; i < messagesForHint.Count; i++) {
            MessageLine messageLine;
            if (i >= messageLineList.Count) {
                messageLine = Instantiate(tmpMessage, content);
                messageLineList.Add(messageLine);
            }
            else {
                messageLine = messageLineList[i];
            }
            
            messageLine.Init(messagesForHint[i]);
            messageLine.Show();
        }
        
        tmpMessage.gameObject.SetActive(false);
    }

    public void CheckCorrect(PeopleSO peopleSO) {
        foreach (var messageLine in messageLineList) {
            if (messageLine.gameObject.activeSelf && messageLine.RemovePeople(peopleSO)) {
                messageLine.Hide();
            }
        }
    }

    public void HideAllLine() {
        foreach (var messageLine in messageLineList) {
            messageLine.gameObject.SetActive(false);
        }
    }
}
