using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectible
{
    public int experienceGranted;

    public void collect()
    {
        PlayerState player = FindObjectOfType<PlayerState>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }

}
