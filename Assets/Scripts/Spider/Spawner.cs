using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 12f;
    float nextSpawnTime;
    [SerializeField] SpiderController spiderPrefab;
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;

    public void Destroy()
    {
        _full.gameObject.SetActive(false);
        _damaged.gameObject.SetActive(true);
        _particleSystem.Play();
    }
    void Update()
    {
        if (readyToSpawn())
        {
            StartCoroutine(spawn());
        }
    }
    IEnumerator spawn()
    {
        nextSpawnTime = Time.time + spawnDelay;

        _full.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        Destroy();
        var spider = Instantiate(spiderPrefab, transform.position, transform.rotation);
    
  
    }
    bool readyToSpawn()
    {
        if (Time.time >= nextSpawnTime)
        {
            return true;
        }
        return false;
    }
   
}
