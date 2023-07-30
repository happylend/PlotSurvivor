using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    int KillNum = 0;
    int WeaponLevel = 0;
    int WeaponLevelCup = 0;
    int LevelNum = 0;
    int ExperienceNum = 0;
    int ExperienceCupNum = 0;
    float hp = 0;

    public Text killText;
    public Text weaponLevelText;
    public Text weaponLevelCupText;
    public Text levelText;
    public Text experienceText;
    public Text experienceCupText;
    public Text hpText;

    WeaponController weaponController;
    PlayerState playerState;
    // Start is called before the first frame update
    void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();
        playerState= FindObjectOfType<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        KillNum = weaponController.experience;
        killText.text = KillNum.ToString();

        WeaponLevelCup = weaponController.experienceCap;
        weaponLevelCupText.text = WeaponLevelCup.ToString();

        WeaponLevel = weaponController.WeaponLevel;
        weaponLevelText.text = WeaponLevel.ToString();

        LevelNum = playerState.level;
        levelText.text = LevelNum.ToString();

        ExperienceNum = playerState.experience;
        experienceText.text = ExperienceNum.ToString();

        ExperienceCupNum = playerState.experienceCap;
        experienceCupText.text = ExperienceCupNum.ToString();

        hp = playerState.CurrentHealth;
        hpText.text = hp.ToString();

    }
}
