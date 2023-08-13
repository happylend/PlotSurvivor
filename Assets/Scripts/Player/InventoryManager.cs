using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>();
    public int[] weaponLevels = new int[6];

    public List<PassiveItem> passiveSlots = new List<PassiveItem>();
    public int[] passiveItemLevels = new int[6];

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponData weaponData;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public int passiveItemUpgradeIndex;
        public GameObject initialPassiveItem;
        public PassiveItemData passiveItemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public Text upgradeNameDisplay;
        public Text upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradesOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradesOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    PlayerState player;


    void Start()
    {
        player = FindObjectOfType<PlayerState>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;

        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveSlots[slotIndex] = passiveItem;

        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            if (!weapon.weaponData._nextLevelPrefab)
            {
                return;
            }
            GameObject upgradeWeapon = Instantiate(weapon.weaponData._nextLevelPrefab, transform.position, Quaternion.identity);
            upgradeWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().weaponData._level;

            weaponUpgradesOptions[upgradeIndex].weaponData = upgradeWeapon.GetComponent<WeaponController>().weaponData;

            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItem(int slotIndex, int upgradeIndex)
    {
        if (passiveSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveSlots[slotIndex];
            if (!passiveItem.passiveItemData._nextLevelPrefab)
            {
                return;
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData._nextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData._level;

            passiveItemUpgradesOptions[upgradeIndex].passiveItemData = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData;


            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradesOptions);
        List<PassiveItemUpgrade> availablePassiveItemUpgrades = new List<PassiveItemUpgrade>(passiveItemUpgradesOptions);

        foreach (var upgradeOption in upgradeUIOptions)
        {
            if (availableWeaponUpgrades.Count == 0 && availablePassiveItemUpgrades.Count == 0)
            {
                return;
            }

            int upgradeType;

            if (availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }
            else if (availablePassiveItemUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);//选择武器升级或者宝珠
            }


            switch(upgradeType)
            {
                case 1:
                    //删除重复升级
                    WeaponUpgrade choseWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
                    availableWeaponUpgrades.Remove(choseWeaponUpgrade);

                    if(choseWeaponUpgrade!=null)
                    {
                        EnableUpgradeUI(upgradeOption);

                        bool newWeapon = false;
                        for (int i = 0; i < weaponSlots.Count; i++)
                        {
                            if (weaponSlots[i] != null && weaponSlots[i].weaponData == choseWeaponUpgrade.weaponData)
                            {
                                newWeapon = false;
                                if(!newWeapon)
                                {
                                    if (!choseWeaponUpgrade.weaponData._nextLevelPrefab)
                                    {
                                        DisableUpgradeUI(upgradeOption);
                                        break;
                                    }

                                    upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, choseWeaponUpgrade.weaponUpgradeIndex));
                                    //获取武器升级后的名称和描述
                                    upgradeOption.upgradeDescriptionDisplay.text = choseWeaponUpgrade.weaponData._Description;
                                    upgradeOption.upgradeNameDisplay.text = choseWeaponUpgrade.weaponData._Name;
                                }
                                break;
                            }
                            else
                            {
                                newWeapon = true;
                            }
                        }
                        if (newWeapon) 
                        {
                            upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(choseWeaponUpgrade.initialWeapon));
                            upgradeOption.upgradeDescriptionDisplay.text = choseWeaponUpgrade.weaponData._Description;
                            upgradeOption.upgradeNameDisplay.text = choseWeaponUpgrade.weaponData._Name;
                        }

                        upgradeOption.upgradeIcon.sprite = choseWeaponUpgrade.weaponData._Icon;
                    }
                    break;
                case 2:
                    //删除重复升级
                    PassiveItemUpgrade chosePassiveItemUpgrade = availablePassiveItemUpgrades[Random.Range(0, availablePassiveItemUpgrades.Count)];
                    availablePassiveItemUpgrades.Remove(chosePassiveItemUpgrade);

                    if (chosePassiveItemUpgrade != null)
                    {
                        EnableUpgradeUI(upgradeOption);

                        bool newPassiveItem = false;
                        for (int i = 0; i < passiveSlots.Count; i++)
                        {
                            if (passiveSlots[i] != null && passiveSlots[i].passiveItemData == chosePassiveItemUpgrade.passiveItemData)
                            {
                                newPassiveItem = false;
                                if (!newPassiveItem)
                                {
                                    if (!chosePassiveItemUpgrade.passiveItemData._nextLevelPrefab)
                                    {
                                        DisableUpgradeUI(upgradeOption);
                                        break;
                                    }

                                    upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i, chosePassiveItemUpgrade.passiveItemUpgradeIndex));
                                    upgradeOption.upgradeDescriptionDisplay.text = chosePassiveItemUpgrade.passiveItemData._nextLevelPrefab.GetComponent<PassiveItem>().passiveItemData._Description;
                                    upgradeOption.upgradeNameDisplay.text = chosePassiveItemUpgrade.passiveItemData._nextLevelPrefab.GetComponent<PassiveItem>().passiveItemData._Name;
                                }
                                break;
                            }
                            else
                            {
                                newPassiveItem = true;
                            }
                        }
                        if (newPassiveItem)
                        {
                            upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosePassiveItemUpgrade.initialPassiveItem));
                            upgradeOption.upgradeDescriptionDisplay.text = chosePassiveItemUpgrade.passiveItemData._Description;
                            upgradeOption.upgradeNameDisplay.text = chosePassiveItemUpgrade.passiveItemData._Name;
                        }

                        upgradeOption.upgradeIcon.sprite = chosePassiveItemUpgrade.passiveItemData._Icon;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption);    //在升级前关闭所有升级选项
        }
    }

    public void RemoveAndApplyupgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);

    }

    void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }

}
