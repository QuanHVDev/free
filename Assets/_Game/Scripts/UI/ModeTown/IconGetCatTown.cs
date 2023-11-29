using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconGetCatTown : MonoBehaviour
{
    [SerializeField] protected Image iconBG;
    [SerializeField] protected Image imgAvatar;
    [SerializeField] protected TMP_Text txtName;
    [SerializeField] private Image imgAds;
    [SerializeField] private Button pickButton;
    private PeopleSO data;
    private bool isAds;

    protected void Awake()
    {
        pickButton.onClick.AddListener(() =>
        {
            if (data)
            {
                ModeTownManager.Instance.SelectOption(data, isAds);
            }
        });
    }

    public void Init(PeopleSO data, bool isAds)
    {
        imgAds.gameObject.SetActive(isAds);
        this.data = data;
        imgAvatar.sprite = data.avatar;
        txtName.text = data.name;
        this.isAds = isAds;
    }
}
