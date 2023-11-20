

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCondition : Condition
{
    protected override void OnMouseDown()
    {
        if (!CheckPrevCondition()) return;
        IsCanShow = true;
        DoneCorrect();
    }

    protected override void DoneCorrect()
    {
        EnableObjectsToHide(false);
        EnableObjectsToShow(true);
    }
}
