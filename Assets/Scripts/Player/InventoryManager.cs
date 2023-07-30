using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>();
    public int[] weaponLevels = new int[6];

    public List<PassiveItem> passiveSlots = new List<PassiveItem>();
    public int[] passiveItemLevels = new int[6];

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveSlots[slotIndex] = passiveItem;
    }

    public void LevelUpWeapon(int slotIndex)
    {

    }

    public void LevelUpPassiveItem(int slotIndex)
    {

    }

}
