using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    private float lifeTime, maxLifeTime = 3f;
    private void OnEnable()=>lifeTime = 0f;
  
    public void launch(Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().velocity=direction*speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="spider")WeaponController.Instance.returnToPool(this);
    }
    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime)WeaponController.Instance.returnToPool(this);
    }
}
