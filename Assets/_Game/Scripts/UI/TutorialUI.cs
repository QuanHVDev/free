using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TMP_Text txtSwipe;
    [SerializeField] private Button btnClick;
    [SerializeField] private Transform hint;

    public void Start()
    {
        btnClick.onClick.AddListener(() =>
        {
            TurnOffAll();
        });
    }

    void TurnOffAll()
    {
        txtSwipe.gameObject.SetActive(false);
        btnClick.gameObject.SetActive(false);
        hint.gameObject.SetActive(false);
        if (iconsFake != null && iconsFake.Count > 0) {
            foreach (var icon in iconsFake) {
                icon.gameObject.SetActive(false);
            }
        }
    }

    public void ShowSwipe(bool enable)
    {
        txtSwipe.gameObject.SetActive(enable);
        if (enable)
        {
            btnClick.gameObject.SetActive(enable);
            txtSwipe.DOFade(0, 1f).SetLoops(-1);
        }
    }

    public void ShowHint()
    {
        btnClick.gameObject.SetActive(true);
        hint.gameObject.SetActive(true);
        iconsFake = new List<Transform>();

        var s1 = SpawnFakeObject(startPositionHint);
        var s2 = SpawnFakeObject(endPositionHint);
        iconsFake.Add(s1);
        iconsFake.Add(s2);
        
        hint.SetSiblingIndex(transform.childCount-1);
        hint.DOMove(endPositionHint.position, 1f).SetLoops(-1);
    }

    private Transform SpawnFakeObject(Transform target)
    {
        var s = Instantiate(target, transform);
        RectTransform sRect = s.GetComponent<RectTransform>();
        sRect.anchorMin = new Vector2(0.5f, 0.5f);
        sRect.anchorMax = new Vector2(0.5f, 0.5f);
        sRect.sizeDelta = target.GetComponent<RectTransform>().sizeDelta;
        sRect.position = target.position;
        
        RemoveRaycast(s);
        foreach (Transform child in s) {
            RemoveRaycast(child);
        }
        return s;
    }

    private void RemoveRaycast(Transform target)
    {
        if (target.TryGetComponent(out Image img))
        {
            img.raycastTarget = false;
        }
    }

    public void SetupHint(Transform start, Transform end)
    {
        startPositionHint = start;
        endPositionHint = end;
        hint.position = startPositionHint.position;
    }

    private Transform startPositionHint;
    private Transform endPositionHint;
    private List<Transform> iconsFake;
}
