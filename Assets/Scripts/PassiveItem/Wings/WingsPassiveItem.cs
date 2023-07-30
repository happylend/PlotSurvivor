using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifler()
    {
        player.CurrentMoveSpeed *= 1 + passiveItemData._multipler / 100f;
    }
}
