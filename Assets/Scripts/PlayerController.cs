using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 0.5f;
    private Animator animator;
    public delegate void updateScore();
    public event updateScore updateScoreEvent;
    public bool isSpiderDead = false;
   

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        checkSpiderDead();
    }
   void checkSpiderDead()
    {
        if (isSpiderDead == true)
        {
            updateScoreEvent();
        }
    }
}
