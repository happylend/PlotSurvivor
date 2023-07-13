using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDamage : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        DamagePopup.Create(Vector3.zero, 600, true);
    }

    // Update is called once per frame

}
