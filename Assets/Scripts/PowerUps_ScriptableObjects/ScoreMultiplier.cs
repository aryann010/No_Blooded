using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PowerUps/ScoreMultiplier")]
public class ScoreMultiplier : PowerEffects
{
    public override void Apply(GameObject target)
    {
        if (target.GetComponent<PlayerController>())
        {
            target.GetComponent<PlayerController>().settingUpScoreWhilePowerUp();

        }
    }
}
