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
        hint.DOMove(endPositionHint.position, 1f).SetLoops(-1);
    }

    public void SetupHint(Transform start, Transform end)
    {
        startPositionHint = start;
        endPositionHint = end;
        hint.position = startPositionHint.position;
    }

    private Transform startPositionHint;
    private Transform endPositionHint;
}
