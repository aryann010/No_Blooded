using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text HealthUIText;
    private PlayerController playerController;
    private WeaponController weaponController;
    private Animator animator;
    private int count=0,x=0;
    int scoreCount;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponent<WeaponController>();
        playerController.updateScoreEvent += updateScore;
        playerController.SpawningPowers += SpawningPowers;
        playerController.updateHealthEvent += updateHealthUI;
        playerController.updateUI += UpdateUI;
    }
    private void Start()
    {
        scoreCount = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + scoreCount;
        HealthUIText.text = "Health: " + 100;
        SpawningPowers();
        PlayerPrefs.DeleteAll();
    }
    void SpawningPowers()=>Instantiate(playerController.PowerrUpPrefabs[Random.Range(0,3)]);
    void updateScore()
    {
        if (playerController.isScoreMultiplierOn == true)
        {
            if (playerController.forCheckingInsideMultiplier == true)
            {
                x = PlayerPrefs.GetInt("currentScore", count)+5;
                PlayerPrefs.DeleteKey("currentScore");
            }
            if (x > scoreCount) scoreCount = x;
                       
            playerController.forCheckingInsideMultiplier = false;
            text.text = "Score: " + x;
            highScoreText.text = "High Score: " + scoreCount;
            PlayerPrefs.SetInt("HighScore", scoreCount);
            PlayerPrefs.SetInt("ScoreAfterMultiplier", x);
            x += 5;
        }

        if (playerController.isScoreMultiplierOn == false)

        {
                count++;
                if (playerController.forCheckingInsideNormal == true)
                {
                    count = PlayerPrefs.GetInt("ScoreAfterMultiplier", x);
                    PlayerPrefs.DeleteKey("ScoreAfterMultiplier");
                }
                if (count > scoreCount) scoreCount = count;
          
               
                playerController.forCheckingInsideNormal = false;
                text.text = "Score: " + count;
                highScoreText.text = "High Score: " + scoreCount;
                PlayerPrefs.SetInt("HighScore", scoreCount);
                PlayerPrefs.SetInt("currentScore", count);
        }
    }
    void restoreDelayTime()=>StartCoroutine(restore());
    IEnumerator restore()
    {
        yield return new WaitForSeconds(7);
        weaponController.delay = 0.25f;
    }
    void updateHealthUI()
    {
        if (playerController.currentHealth <= 0)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            animator.SetBool("isPlayerDead",true);
            HealthUIText.text = " Health: " + 0;
            SceneManager.LoadScene(2);
        }
        if (playerController.currentHealth > 0)
        {
            playerController.currentHealth -= 7;
            HealthUIText.text = " Health: " + playerController.currentHealth;
        }
    }
    public void OnButtonExit()=>SceneManager.LoadScene(0);
    void UpdateUI()=>HealthUIText.text = "Health: " + playerController.currentHealth;

    private void OnDestroy()
    {
        playerController.updateScoreEvent -= updateScore;
        playerController.SpawningPowers -= SpawningPowers;
        playerController.updateHealthEvent -= updateHealthUI;
        playerController.updateUI -= UpdateUI;
        PlayerPrefs.DeleteKey("currentScore");
        PlayerPrefs.DeleteKey("ScoreAfterMultiplier");
    }
  public void playAgain()=>SceneManager.LoadScene(1);   
}
