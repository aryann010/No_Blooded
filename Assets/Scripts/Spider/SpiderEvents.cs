using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEvents : MonoBehaviour
{
    [SerializeField] private SpiderController spidercontroller;
    private bool isPlayerinRange = false;

    private void Awake()
    {
        spidercontroller.dieEvent += die;
        spidercontroller.takeHitEvent += takeHit;
        spidercontroller.attackEvent += attack;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent < PlayerController>())isPlayerinRange = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())isPlayerinRange = false;
    }
    void die()
    {
        GetComponent<Collider>().enabled = false;
       spidercontroller.navmeshagent.enabled = false;
       spidercontroller.animator.SetTrigger("dead");
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
        if(isPlayerinRange)
        spidercontroller.attackedPlayer();
    }
    void AttackHit() { }
    private void OnDestroy()
    {
        spidercontroller.dieEvent -= die;
        spidercontroller.takeHitEvent -= takeHit;
        spidercontroller.attackEvent -= attack;
    }
}
