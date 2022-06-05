using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PowerUps/MaxHealth")]
public class MaxHealth : PowerEffects
{
    private int health;
    public override void Apply(GameObject target)
    {
        if (target.GetComponent<PlayerController>())
        {
            target.GetComponent<PlayerController>().currentHealth = 100;
        }
    }
}
