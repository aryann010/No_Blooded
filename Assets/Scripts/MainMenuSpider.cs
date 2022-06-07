using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MainMenuSpider : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] GameObject destination;
    private void Awake()=>navMeshAgent = GetComponent<NavMeshAgent>();
    void Update()=>move();
    private void move()=>navMeshAgent.SetDestination(destination.transform.position);
    public void playButton()=>SceneManager.LoadScene(1);
}
