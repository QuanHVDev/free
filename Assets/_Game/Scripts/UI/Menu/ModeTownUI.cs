using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeTownUI : BaseUIElement
{
    public override void OnAwake()
    {
    }
    
    [SerializeField] private TMP_Text txtNameTown;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnLeftArrow;
    [SerializeField] private Button btnRightArrow;
    [SerializeField] private Transform townDetails;

    [SerializeField] private CanvasGroup adoptGroup;
    [SerializeField] private Button btnAddCat;
    [SerializeField] private ProcessBar processBar;
    [SerializeField] private DotweenRunAnim gift;
    [SerializeField] private TMP_Text txtPriceRollCat;

    private void Start()
    {
        btnAddCat.onClick.AddListener(ModeTownManager.Instance.AddCatTown);
        GetCatUI.Hide();
        adoptUI.gameObject.SetActive(false);
        iconCatSelected.SetSelect(false);
        iconCatSelected.OnComplete = CompleteMovingSelected;
    }

    public void Init()
    {
        btnExit.onClick.AddListener(ModeTownManager.Instance.OutModeTown);
        btnLeftArrow.onClick.AddListener(ModeTownManager.Instance.BackToBeforeTown);
        btnRightArrow.onClick.AddListener(ModeTownManager.Instance.NextToAfterTown);
        
        ModeTownManager.Instance.OnBeforeDoMoveIsland += ModeTownManager_OnBeforeDoMoveIsland;
        ModeTownManager.Instance.OnAfterDoMoveIsland += ModeTownManager_OnAfterDoMoveIsland;
        ModeTownManager.Instance.OnInModeTown += () =>
        {
            ModeTownManager_OnInModeTown();
            Show();
        };
        ModeTownManager.Instance.OnOutModeTown += () =>
        {
            ModeTownManager_OnOutModeTown();
            Hide();
        };
        
        Hide();
        processBar.Init();
    }

    public void SetTextRollCat(int price)
    {
        txtPriceRollCat.text = price.ToString();
    }

    public void SetValueForProcessBar(float precent)
    {
        processBar.gameObject.SetActive(true);
        processBar.SetSmooth(precent);
        if (precent >= 1)
        {
            gift.RunAnim();
        }
        else
        {
            gift.StopAnim();
        }
    }

    public void HideProcessBar()
    {
        processBar.gameObject.SetActive(false);
    }

    private void ModeTownManager_OnOutModeTown()
    {
        adoptGroup.blocksRaycasts = false;
        adoptUI.Hide();
    }

    private void ModeTownManager_OnInModeTown()
    {
        adoptGroup.blocksRaycasts = true;
    }

    private void ModeTownManager_OnAfterDoMoveIsland(int currentLevel, int maxLevel)
    {
        if (currentLevel > 0)
            btnLeftArrow.gameObject.SetActive(true);

        if (currentLevel < maxLevel)
            btnRightArrow.gameObject.SetActive(true);

        townDetails.gameObject.SetActive(true);
    }

    private void ModeTownManager_OnBeforeDoMoveIsland(int currentLevel, int maxLevel)
    {
        btnLeftArrow.gameObject.SetActive(false);
        btnRightArrow.gameObject.SetActive(false);
        townDetails.gameObject.SetActive(false);
    }

    [Header("Add Cat")]     
    [SerializeField] private GetCatUI GetCatUI;
    [SerializeField] private IconGetCatTown iconGetCatTown;
    [SerializeField] private Transform contentGetCat;
    private List<IconGetCatTown> catTowns;
    private readonly int amountAds = 1;

    public void InitAddCat(List<PeopleSO> data)
    {
        iconGetCatTown.gameObject.SetActive(false);
        GetCatUI.Show();
        var cats = new List<PeopleSO>(data); 
        
        for (int i = 0; i < data.Count; i++)
        {
            var item = GetCatTown(i);
            item.gameObject.SetActive(true);
            item.Init(data[i], amountAds - i > 0);
        }
    }

    private IconGetCatTown GetCatTown(int index)
    {
        if (catTowns == null) catTowns = new List<IconGetCatTown>();
        if (index >= catTowns.Count)
        {
            var item = Instantiate(iconGetCatTown, contentGetCat);
            catTowns.Add(item);
            return item;
        }

        return catTowns[index];
    }
    
    public void HideAddCatUI()
    {
        GetCatUI.Hide();
    }


    [Header("Selection")] 
    [SerializeField] private SelectionCatBarUI selectionCatBarUI;
    [SerializeField] private IconCatSelection iconCatSelection;
    [SerializeField] private Transform contentSelection;
    [SerializeField] private ScrollRect scrollRectSelection;
    [SerializeField] private TMP_Text txtCountSelection;
    private List<IconCatSelection> catTownSelections;
    private int countSelection;
    public void InitSelectionCat(List<PeopleSO> data)
    {
        iconCatSelection.gameObject.SetActive(false);
        selectionCatBarUI.Show();
        for (int i = 0; i < data.Count; i++)
        {
            var item = GetCatSelectionObject(i);
            item.gameObject.SetActive(true);
            item.Init(data[i]);
        }
        
    }
    
    

    public void AddSelectionCat(PeopleSO data)
    {
        var item = GetCatSelectionObject();
        item.gameObject.SetActive(true);
        item.Init(data);
        SetTextCoutSelection(++countSelection);
    }
    
    
    private IconCatSelection GetCatSelectionObject(int index = -1)
    {
        if (catTownSelections == null) catTownSelections = new List<IconCatSelection>();
        if (index >= catTownSelections.Count ||index == -1)
        {
            var item = Instantiate(iconCatSelection, contentSelection);
            catTownSelections.Add(item);
            return item;
        }
        
        foreach (var icon in catTownSelections)
        {
            if (icon.Data == null)
                return icon;
        }    

        return catTownSelections[index];
    }
    
    

    public void EnableVerticalScroll(bool enable)
    {
        scrollRectSelection.vertical = enable;
    }

    public void SetTextCoutSelection(int number)
    {
        txtCountSelection.text = number.ToString();
    }

    [Header("Selected")] 
    [SerializeField] private IconCatSelected iconCatSelected;

    public IconCatSelected SetSelected(PeopleSO data)
    {
        iconCatSelected.Init(data);
        return iconCatSelected;        

    }

    private readonly float distance = 100f;
    // ReSharper disable Unity.PerformanceAnalysis
    private void CompleteMovingSelected()
    {
        if (!adoptUI.gameObject.activeSelf)
        {
            SFX.Instance.PlayIncorrect();
            EnableVerticalScroll(true);
            return;
        }
        
        IconAdopt iconMin = new IconAdopt();
        float min = float.MaxValue;
        int index = -1;
        for (int i = 0; i < adoptUI.RequestIcons.Count; i++)
        {
            float value = Vector3.Distance(iconCatSelected.transform.position, adoptUI.RequestIcons[i].transform.position);
            if (value < distance && adoptUI.RequestIcons[i].enabled && value < min)
            {
                iconMin = adoptUI.RequestIcons[i];
                min = value;
                index = i;
            }
        }

        if (min < float.MaxValue && adoptUI.CheckCatWithTags(iconCatSelected.Data.Tags, iconMin))
        {
            if (ModeTownManager.Instance.SaveCorrect(index, iconCatSelected.Data))
            {
                iconCatSelected.GetIconSelectionPrev().Reset();
            }
        }
        else {
            SFX.Instance.PlayIncorrect();
            EnableVerticalScroll(true);
        }
    }

    [Header("Adopt")] 
    [SerializeField] private AdoptUI adoptUI;
    public void ShowRequestHouse(List<TagCat> tagCats, House houseTransform)
    {
        adoptUI.Show();
        adoptUI.ShowRequest(tagCats, houseTransform);
    }

    public void HideAdoptUI()
    {
        adoptUI.Hide();
    }

    public void ShowFilledAdopt(int indexTag, PeopleSO data, bool isCanUndo)
    {
        adoptUI.FilledAdopt(indexTag, data, isCanUndo);
    }

    public void ResetFilledAdopt()
    {
        adoptUI.ResetFilledAdopt();
    }

    public void SetTextTownName(string name)
    {
        txtNameTown.text = name;
    }
}