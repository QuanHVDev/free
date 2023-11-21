

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCondition : Condition
{
    protected override void OnMouseDown()
    {
        if (!IsCheckPrevCondition()) return;
        StartCoroutine(DoneCorrectAsync());
    }
}
