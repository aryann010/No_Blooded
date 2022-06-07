using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 12f;
    [SerializeField] SpiderController spiderPrefab;
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;
    float nextSpawnTime;
    private Queue<SpiderController> spiders = new Queue<SpiderController>();
    public static Spawner Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (readyToSpawn())
        {
            nextSpawnTime = Time.time + spawnDelay;
            if (spiders.Count > 0)
            {
                _full.gameObject.SetActive(true);
                Destroy();
                var Spider = spiders.Dequeue();
                Spider.GetComponent<Collider>().enabled = true;
                Spider.navmeshagent.enabled = true;
                Spider.gameObject.SetActive(true);
                Spider.currentHealth = 100;
                Spider.transform.position = transform.position;
                Spider.navmeshagent.SetDestination(Spider.player.transform.position);
            }
            else 
            { 
                _full.gameObject.SetActive(true);
                Destroy();
                var spider = Instantiate(spiderPrefab, transform.position, transform.rotation);
            }
        }
    }
    public void Destroy()
    {
        _full.gameObject.SetActive(false);
        _damaged.gameObject.SetActive(true);
        _particleSystem.Play();
    }
    bool readyToSpawn()
    {
        if (Time.time >= nextSpawnTime)return true;
        else return false;
    }
    public void returnToPool(SpiderController spiderPrefab)
    {
        spiderPrefab.gameObject.SetActive(false);
        spiders.Enqueue(spiderPrefab);
    }
}
