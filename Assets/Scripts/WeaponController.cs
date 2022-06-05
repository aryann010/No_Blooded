using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    float nextFireTime;
  [SerializeField] float delay=0.25f;
    [SerializeField] BulletController bulletController;
    private Queue<BulletController> bullets = new Queue<BulletController>();
    public static WeaponController Instance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform firePoint;

  
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    void Update()
    {
        aimDirection(); 
            if (bullets.Count == 0)
            {
                if (ReadyToFire())
                    Fire();
            }
            else
            {
                if (ReadyToFire())
                {

                    BulletController enqBullet = bullets.Dequeue();
                    nextFireTime = Time.time + delay;
                    enqBullet.gameObject.transform.position = firePoint.position;
                    enqBullet.gameObject.transform.rotation = Quaternion.Euler(transform.forward);
                    enqBullet.gameObject.SetActive(true);

                    enqBullet.launch(transform.forward);
                }
            }
       
    }
    void aimDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity,layerMask))
        {
            var destination = hitInfo.point;
            destination.y = transform.position.y;
            Vector3 direction = destination - transform.position;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        } 
    }
    bool ReadyToFire()
    {
        if (Time.time >= nextFireTime && Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
    void Fire()
    {

        nextFireTime = Time.time + delay;
        var shot = Instantiate(bulletController, firePoint.position, Quaternion.Euler(transform.forward));
        shot.launch(transform.forward);
        bullets.Enqueue(shot);
    }
    public void returnToPool(BulletController bulletPrefab)
    {
        bulletPrefab.gameObject.SetActive(false);

        bullets.Enqueue(bulletPrefab);
    }
}
