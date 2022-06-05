using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text highScoreText;
    private PlayerController playerController;
    private WeaponController weaponController;

    
    private int count=1,x=5;
   
    int scoreCount;
    private void Awake()
    {
        // spiderController = GetComponent<SpiderController>();
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponent<WeaponController>();
        playerController.dieEvent += OnDieEvent;
        playerController.updateScoreEvent += updateScore;

   
      
    }
    private void Start()
    {
        scoreCount = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score : " + scoreCount;
    }
    void updateScore()
    {
       
      
      
        if (playerController.isScoreMultiplierOn == false)
        {
           
            if (playerController.forCheckingInsideNormal == true)
            {
                count = PlayerPrefs.GetInt("ScoreAfterMultiplier", x);
                count++;
                PlayerPrefs.DeleteKey("ScoreAfterMultiplier");
            }
            if (count > scoreCount)
            {
                scoreCount = count;
            }
            playerController.forCheckingInsideNormal = false;
            text.text = "Score : " + count;
            highScoreText.text = "High Score : " + scoreCount;
            PlayerPrefs.SetInt("HighScore", scoreCount);
            PlayerPrefs.SetInt("currentScore", count);
            count++;
        }
        if (playerController.isScoreMultiplierOn == true)
        {
            if (playerController.forCheckingInsideMultiplier == true)
            {
                x += PlayerPrefs.GetInt("currentScore", count);
                PlayerPrefs.DeleteKey("currentScore");

            }
            if (x > scoreCount)
            {
                scoreCount = x;
            }
            playerController.forCheckingInsideMultiplier = false;
            text.text = "Score : " + x;
            highScoreText.text = "High Score : " + scoreCount;
            PlayerPrefs.SetInt("HighScore",scoreCount);
            PlayerPrefs.SetInt("ScoreAfterMultiplier", x);
            x+=5;
        }
    }
    void OnDieEvent()
    {
        //player will die animated
    }
    void restoreDelayTime()
    {
        StartCoroutine(restore());
    }
    IEnumerator restore()
    {
        yield return new WaitForSeconds(7);
        weaponController.delay = 0.25f;
    }
    private void OnDestroy()
    {

        playerController.updateScoreEvent -= updateScore;
        playerController.dieEvent -= OnDieEvent;
    }
}
