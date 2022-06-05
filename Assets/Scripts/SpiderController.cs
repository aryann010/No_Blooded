using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderController : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] float health = 100;
    public float currentHealth;
   public NavMeshAgent navmeshagent;
    public Animator animator;
    public delegate void die();
    public event die dieEvent;
    public delegate void takeHit();
    public event takeHit takeHitEvent;
    public delegate void attack();
    public event attack attackEvent;
    private PlayerController player;
    private WeaponController weapon;
  


    void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
         player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        currentHealth = health;
        navmeshagent.enabled = true;
  
     
    }
    void Update()
    {
        
        if (currentHealth <= 0) { return; }
       if(navmeshagent.enabled) navmeshagent.SetDestination(player.transform.position);
       if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            attackEvent();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       var shot = collision.gameObject.GetComponent<BulletController>();
        if (shot != null)
        {
            currentHealth -= 33;
            if (currentHealth <= 0)
            { 
                dieEvent();

                player.isSpiderDead = true;


            }
        else
            takeHitEvent();
        }
    }
}
