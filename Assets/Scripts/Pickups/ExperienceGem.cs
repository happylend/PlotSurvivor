using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : PickUp
{
    public int experienceGranted;

    public override void collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.collect();
        }

        PlayerState player = FindObjectOfType<PlayerState>();
        player.IncreaseExperience(experienceGranted);

    }

}
