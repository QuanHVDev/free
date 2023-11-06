using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBar : MonoBehaviour {
    [SerializeField] private Slider _sliderProcess;

    private void Start()
    {
        _sliderProcess.value = 0;
    }

    public void SetSmooth(float newPercent) {
        if (_sliderProcess.value < newPercent) {
            StartCoroutine(SetSmoothIncAsync(newPercent));
        }
        else {
            StartCoroutine(SetSmoothDescAsync(newPercent));
        }
    }

    private IEnumerator SetSmoothIncAsync(float newPercent)
    {
        float currentFill = _sliderProcess.value;
        float changeFill = newPercent - currentFill;

        while (newPercent - currentFill > Mathf.Epsilon)
        {
            currentFill += changeFill * Time.deltaTime;
            _sliderProcess.value = currentFill;
            yield return null;
        }

        _sliderProcess.value = currentFill;
    }
    
    private IEnumerator SetSmoothDescAsync(float newPercent)
    {
        float currentFill = _sliderProcess.value;
        float changeFill = currentFill - newPercent;

        while (currentFill - newPercent > Mathf.Epsilon)
        {
            currentFill -= changeFill * Time.deltaTime;
            _sliderProcess.value = currentFill;
            yield return null;
        }

        _sliderProcess.value = currentFill;
    }
}
