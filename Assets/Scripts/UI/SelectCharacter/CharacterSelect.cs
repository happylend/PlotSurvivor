using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public static CharacterSelect instance;
    public AttributeBase characterData;
    public WeaponData weaponData;

    void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public static AttributeBase GetData()
    {
        return instance.characterData;
    }

    public static WeaponData GetWeapon()
    {
        return instance.weaponData;
    }

    public void SelectCharacter(AttributeBase character)
    {
        characterData = character;

    }

    public void SelecetWeapon(WeaponData weapon)
    {
        weaponData = weapon;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
