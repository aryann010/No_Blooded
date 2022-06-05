using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 0.5f;
    [SerializeField] float health = 100;
    public float currentHealth;
    private Animator animator;
    public delegate void DieEvent();
    public event DieEvent dieEvent;
    public bool isScoreMultiplierOn = false;
    public bool forCheckingInsideMultiplier = true;
    public bool forCheckingInsideNormal = false;
    public delegate void updateScore();
    public event updateScore updateScoreEvent;
    [SerializeField] TMP_Text text;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
    }
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
        if (collision.gameObject.GetComponent<SpiderController>())
        {
            currentHealth -= 9;
            if (currentHealth <= 0)
            {
                dieEvent();
            }
        }
    }
   public void settingUpScoreWhilePowerUp()
    {
        isScoreMultiplierOn = true;
        text.gameObject.SetActive(true);
        StartCoroutine(timerForMultiplier());
        text.gameObject.SetActive(false);
    }
    IEnumerator timerForMultiplier()
    {
        yield return new WaitForSeconds(15);
        isScoreMultiplierOn = false;
        forCheckingInsideNormal = true;
    }
    public void forScoreMultiplier()
    {
        updateScoreEvent();
    }
}
