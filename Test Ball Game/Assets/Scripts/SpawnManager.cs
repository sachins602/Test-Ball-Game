﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public GameObject waveObject;
    private float spawnRange = 7.0f;
    public int enemyCount;
    public int waveNumber = 0;
    private int score;
    private GameObject[] powerUpCount;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI waveCounterText;
    //public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScoreText;
    //public TextMeshProUGUI highScoreTextHome;
    //private GameStart gameStart;
    public Button restartButton;
    public Button mainMenuButton; 
    public bool gameOver;

    public GameObject inGameMenu;
    public GameObject gameOverMenu;
    
   // private AudioSource spawnAudio;
   // public AudioClip gameOverAudio;

    // Start is called before the first frame update
    void Start()
    {
        //highScoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
      // highScoreTextHome.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        SpawnEnemyWave(waveNumber);
        Instantiate(powerUpPrefab, RandomPostionGenerator(), powerUpPrefab.transform.rotation);
        AdManager.instance.RequestBanner();
        AdManager.instance.ShowBannerAd();

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        powerUpCount = GameObject.FindGameObjectsWithTag("PowerUp");
        int powerCount = powerUpCount.Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            waveCounterText.text = waveNumber.ToString();
            SpawnEnemyWave(waveNumber);
            if (powerCount < 1)
            {
                Instantiate(powerUpPrefab, RandomPostionGenerator(), powerUpPrefab.transform.rotation);
            }
        }
    }
    void SpawnEnemyWave(int enemyToSpawn)
    {
       
            
            for (int i = 0; i < enemyToSpawn; i++)
            {
            if (enemyToSpawn < 6)
            {

                Instantiate(enemyPrefab, RandomPostionGenerator(), enemyPrefab.transform.rotation);
            }
            else if(enemyToSpawn >= 6)
            {
                enemyToSpawn--;
                Instantiate(enemyPrefab, RandomPostionGenerator(), enemyPrefab.transform.rotation);

            }
            }
        
    }
    private Vector3 RandomPostionGenerator()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);
        return randomPos;
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
        finalScoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            //highScoreText.text = "HIGHSCORE: " + score;
            //highScoreTextHome.text = "HIGHSCORE: " + score;
            CloudOnceServices.instance.SubmitScoreToLeaderboard(PlayerPrefs.GetInt("HighScore"));
        }

    }
    public void GameOver()
    {
        AdManager.instance.HideBannerAd();
        AdManager.instance.ShowInterstitialAd();
        //spawnAudio.PlayOneShot(gameOverAudio, 1.0f);
        inGameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        //highScoreText.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       Time.timeScale = 1;
   }
}
