using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCondition : Condition
{
    public override void ClickObject()
    {
        base.ClickObject();
        IsCanShow = true;
    }
}
