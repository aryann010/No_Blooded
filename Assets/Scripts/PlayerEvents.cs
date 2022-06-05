using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private PlayerController playerController;
    private int count=1;
    private void Awake()
    {
        // spiderController = GetComponent<SpiderController>();
        playerController = GetComponent<PlayerController>();
        playerController.updateScoreEvent += updateScore;
      
    }
   void updateScore()
    {
        playerController.isSpiderDead = false;
        text.text = "Score : " + count;
        count++;
        
       
    }
    private void OnDestroy()
    {

        playerController.updateScoreEvent -= updateScore;
    }
}
