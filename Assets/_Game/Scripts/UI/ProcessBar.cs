using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBar : MonoBehaviour {
    [SerializeField] private RectTransform fillerParent;
    [SerializeField] private RectTransform filler;

    public void Init()
    {
        filler.sizeDelta = new Vector2(0f, filler.sizeDelta.y);
    }

    public void SetSmooth(float newPercent)
    {
        if (!gameObject.activeSelf) return;
        if (filler.sizeDelta.x / fillerParent.sizeDelta.x < newPercent) {
            StartCoroutine(SetSmoothIncAsync(newPercent));
        }
        else {
            StartCoroutine(SetSmoothDescAsync(newPercent));
        }
    }

    private IEnumerator SetSmoothIncAsync(float newPercent)
    {
        float currentFill = filler.sizeDelta.x / fillerParent.sizeDelta.x;
        float changeFill = newPercent - currentFill;

        while (newPercent - currentFill > Mathf.Epsilon)
        {
            currentFill += changeFill * Time.deltaTime;
            filler.sizeDelta = new Vector2(currentFill * fillerParent.sizeDelta.x, filler.sizeDelta.y);;
            yield return null;
        }

        filler.sizeDelta = new Vector2(currentFill * fillerParent.sizeDelta.x, filler.sizeDelta.y);
    }
    
    private IEnumerator SetSmoothDescAsync(float newPercent)
    {
        float currentFill = filler.sizeDelta.x / fillerParent.sizeDelta.x;
        float changeFill = currentFill - newPercent;

        while (currentFill - newPercent > Mathf.Epsilon)
        {
            currentFill -= changeFill * Time.deltaTime;
            filler.sizeDelta = new Vector2(currentFill * fillerParent.sizeDelta.x, filler.sizeDelta.y);;
            yield return null;
        }

        filler.sizeDelta = new Vector2(currentFill * fillerParent.sizeDelta.x, filler.sizeDelta.y);;
    }
}
