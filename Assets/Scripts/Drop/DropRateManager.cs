using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefabs;
        public float dropRate;

    }

    public List<Drops> drops;

    public void DropItem()
    {
        float randomNumber = Random.Range(0f, 100f);

        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }

        if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefabs, transform.position, Quaternion.identity);
        }
    }
}
