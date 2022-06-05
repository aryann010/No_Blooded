using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEvents : MonoBehaviour
{
    [SerializeField] private SpiderController spidercontroller;
   

    private void Awake()
    {
        spidercontroller.dieEvent += die;
        spidercontroller.takeHitEvent += takeHit;
        spidercontroller.attackEvent += attack;
    }
    void die()
    {
        GetComponent<Collider>().enabled = false;
       spidercontroller.navmeshagent.enabled = false;
       spidercontroller.animator.SetTrigger("dead");
      
        Destroy(gameObject, 5f);
 
    }
    void takeHit()
    {
       spidercontroller.navmeshagent.enabled = false;
       spidercontroller.animator.SetLayerWeight(spidercontroller.animator.GetLayerIndex("Base Layer"), 0);
       spidercontroller.animator.SetLayerWeight(spidercontroller.animator.GetLayerIndex("layer1"), 1);
       spidercontroller.animator.SetTrigger("hit");
        StartCoroutine(waiting()); 
    }
    IEnumerator waiting()
    {
        yield return new WaitForSeconds(0.5f);
       spidercontroller.animator.SetLayerWeight(spidercontroller.animator.GetLayerIndex("layer1"), 0);
       spidercontroller.animator.SetLayerWeight(spidercontroller.animator.GetLayerIndex("Base Layer"), 1);
        if (spidercontroller.currentHealth > 0) spidercontroller.navmeshagent.enabled = true;

    }
    private void attack()
    {
       spidercontroller.animator.SetTrigger("isAttack");
       spidercontroller.navmeshagent.enabled = false;
    }
    void AttackComplete()     //anim callback
    {
        if (spidercontroller.currentHealth > 0)
          spidercontroller. navmeshagent.enabled = true;
    }
    void AttackHit()         //anim callback
    {

    }
    private void OnDestroy()
    {
        spidercontroller.dieEvent -= die;
        spidercontroller.takeHitEvent -= takeHit;
        spidercontroller.attackEvent -= attack;
    }
}
