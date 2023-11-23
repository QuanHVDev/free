using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private BaseUIElement[] uiElements;

    private Dictionary<System.Type, BaseUIElement> mapping = new Dictionary<System.Type, BaseUIElement>();

    private static UIRoot instance;

    public static UIRoot Ins => instance;

    private void Awake()
    {
        foreach (var m in uiElements)
        {
            if (instance != null)
                Ins.SetMapping(m);
            else SetMapping(m);
        }

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(gameObject);
    }


    public void SetMapping(BaseUIElement element, bool invokeAwake = true)
    {
        var t = element.GetType();
        if (mapping.ContainsKey(t)) return;

        mapping[t] = element;
        element.Root = this;
        element.transform.SetParent(this.transform);
        element.transform.SetAsFirstSibling();

        if (invokeAwake)
        {
            element.OnAwake();
        }
    }

    public void Remove(BaseUIElement ui)
    {
        if (mapping.TryGetValue(ui.GetType(), out BaseUIElement obj))
        {
            mapping.Remove(ui.GetType());
            Destroy(obj.gameObject);
        }
    }

    public T Get<T>() where T : BaseUIElement
    {
        var type = typeof(T);
        return mapping.TryGetValue(type, out BaseUIElement o) ? (T)o : null;
    }
}

public abstract class BaseUIElement : MonoBehaviour
{
    [SerializeField] Image bg;
    [SerializeField] Transform mainFrame;

    public UIRoot Root { get; set; }
    public abstract void OnAwake();

    public void Show(float toAlpha = 0.75f)
    {
        gameObject.SetActive(true);
        if (bg)
        {
            bg.DOKill();
            var c = bg.color;
            c.a = 0;
            bg.color = c;
            bg.DOFade(toAlpha, .3f).SetUpdate(true);
        }

        if (mainFrame)
        {
            mainFrame.DOKill();
            mainFrame.transform.localScale = Vector3.one * 0.5f;
            mainFrame.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    public virtual void Hide()
    {
        if (bg)
        {
            bg.DOKill();
            bg.DOFade(0, .3f).SetUpdate(true);
        }

        if (mainFrame)
        {
            mainFrame.DOKill();
            mainFrame.DOScale(0, 0.3f).SetUpdate(true).OnComplete(() => { gameObject.SetActive(false); });
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnDestroy()
    {
        RemoveSelf();
    }

    public void RemoveSelf()
    {
        if (Root) Root.Remove(this);
    }
}