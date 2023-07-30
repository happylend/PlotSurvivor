using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItem
{
    protected override void ApplyModifler()
    {
        bullet.currentDamage *= 1 + passiveItemData._multipler / 100f;
    }
}
