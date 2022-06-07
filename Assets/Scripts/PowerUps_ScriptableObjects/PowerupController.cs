using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public PowerEffects powerEffects;
    private Vector3 playerPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
            powerEffects.Apply(other.gameObject);
        }
    }
    void Awake()=>playerPosition = FindObjectOfType<PlayerController>().gameObject.transform.position;
    void Start()=>StartPosition();     
    private void StartPosition()=>transform.position = new Vector3(playerPosition.x +Random.Range(-10,10) , 0, playerPosition.z + Random.Range(-10, 10));

}

   

