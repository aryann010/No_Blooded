using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 0.5f;
    [SerializeField] float health = 100;
    public bool isScoreMultiplierOn = false;
    public bool isNormalShotFired = false;
    public bool forCheckingInsideMultiplier = false;
    public bool forCheckingInsideNormal = false;
    public GameObject[] PowerrUpPrefabs;
    public float currentHealth;
    private Animator animator;
    public delegate void updateScore();
    public event updateScore updateScoreEvent;
    public delegate void spawningPowers();
    public event spawningPowers SpawningPowers;
    public delegate void updateHealthUI();
    public event updateHealthUI updateHealthEvent;
    public delegate void HealthTextUpdateAfterPowerUp();
    public event HealthTextUpdateAfterPowerUp updateUI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
    }
    private void Start()=>SpawningPowers();
   
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement *= Time.deltaTime * Speed;
        transform.Translate(movement, Space.World);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SpiderController>())currentHealth -= 9;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerupController>()) StartCoroutine(forr());
    }
    IEnumerator forr()
    {
        yield return new WaitForSeconds(9);
        SpawningPowers();
    }
    public void settingUpScoreWhilePowerUp()
    {
        isScoreMultiplierOn = true;
        StartCoroutine(timerForMultiplier());
        forCheckingInsideMultiplier = true;
    }
    IEnumerator timerForMultiplier()
    {
        yield return new WaitForSeconds(15);
        isScoreMultiplierOn = false;
        forCheckingInsideMultiplier = false;
        forCheckingInsideNormal = true;
    }
    public void forScoreMultiplier()=>updateScoreEvent();

    public void updateScoreMethod()
    {
       updateHealthEvent();

    }
    public void updateHealthAfterPowerup()=>updateUI(); 
}
