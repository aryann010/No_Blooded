using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PowerUps/MaxBulletSpeed")]
public class MaxBulletSpeed : PowerEffects
{
    public float bulletDelayDecrease;
   
    public override void Apply(GameObject target)
    {
        target.GetComponent<WeaponController>().delay -= bulletDelayDecrease;
        target.GetComponent<WeaponController>().resetDelayTimer();
    }

}
