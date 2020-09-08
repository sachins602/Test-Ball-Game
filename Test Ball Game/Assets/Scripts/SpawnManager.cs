﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private MenuBehaviours menuBehaviours;

    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 7.0f;
    public int enemyCount;
    public int waveNumber = 0;
    private int score;
    private GameObject[] powerUpCount;



    //public TextMeshProUGUI highScoreTextHome;
    //private GameStart gameStart;
   // public Button restartButton;
    //public Button mainMenuButton; 
    
   // private AudioSource spawnAudio;
   // public AudioClip gameOverAudio;

    // Start is called before the first frame update
    void Start()
    {
        menuBehaviours = GameObject.Find("Canvas").GetComponent<MenuBehaviours>();
        menuBehaviours.highScoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
      // highScoreTextHome.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        SpawnEnemyWave(waveNumber);
        Instantiate(powerUpPrefab, RandomPostionGenerator(), powerUpPrefab.transform.rotation);
        
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
            menuBehaviours.waveCounterText.text = "Wave: " + waveNumber;
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
        menuBehaviours.scoreText.text = "SCORE : " + score;
        menuBehaviours.finalScoreText.text = "SCORE : " + score;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            menuBehaviours.highScoreText.text = "HIGHSCORE: " + score;
            //highScoreTextHome.text = "HIGHSCORE: " + score;
        }

    }
    public void GameOver()
    {
        menuBehaviours.gameOverUi.SetActive(true);
        Time.timeScale = 0f;
    }
  
}
