using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public PowerEffects powerEffects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
            powerEffects.Apply(other.gameObject);
        }
     
    }
   
}
