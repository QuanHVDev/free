using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : BaseUIElement
{
    public Action OnHideLine;
    [SerializeField] private Button btnNextLevel;
    [SerializeField] private Button btnGetx2WithAds;
    [SerializeField] private TMP_Text txtCoin;

    private ButtonElement btnNextLevelElement, btnGetx2WithAdsElement;

    public override void OnAwake()
    {
    }

    private void Start()
    {
        btnNextLevel.onClick.AddListener(() =>
        {
            OnHideLine?.Invoke();
            ModeFindCatManager.Instance.NextLevel();
            Hide();
        });
        
        btnGetx2WithAds.onClick.AddListener(() =>
        {
            OnHideLine?.Invoke();
            ModeFindCatManager.Instance.Getx2WithAds();
            btnGetx2WithAds.gameObject.SetActive(false);
        });

        //btnNextLevelElement = btnNextLevel.GetComponent<ButtonElement>();
        //btnGetx2WithAdsElement = btnGetx2WithAdsElement.GetComponent<ButtonElement>();
    }

    public void SetValueDiamond(int value, float timeAnim)
    {
        txtCoin.text = $"+{value}";
        txtCoin.transform.DOPunchScale(Vector3.one * 0.3f, timeAnim);
    }

    public override void Show(float toAlpha = 0.75f)
    {
        base.Show(toAlpha);
        StartCoroutine(ShowAsync());
    }

    private IEnumerator ShowAsync()
    {
        btnNextLevel.gameObject.SetActive(false);
        btnGetx2WithAds.gameObject.SetActive(true);
        //btnGetx2WithAdsElement.Show();
        yield return new WaitForSeconds(3f);
        btnNextLevel.gameObject.SetActive(true);
        //btnNextLevelElement.Show();
    }
        
}