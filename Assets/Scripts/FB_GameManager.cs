﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FB_GameManager : MonoBehaviour
{
    public static FB_GameManager Instance;

    public delegate void gameDelegate();
    public static event gameDelegate OnGameStarted;
    public static event gameDelegate OnGameOverConfirmed;


    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public Text scoreText;

    enum PageState {
        None,
        Start,
        GameOver,
        CountDown
    }

    int score = 0;
    bool gameOver = true;
    public bool GameOver { get { return gameOver; } }
    public int Score { get { return score; }}
   
   void Awake()
   {
       Instance = this;
   }

   void OnEnable()
   {
       CountDownText.OnCountdownFinished += OnCountdownFinished; 
       FB_PlayerMovement.OnPlayerDied += OnPlayerDied;
       FB_PlayerMovement.OnPlayerScored += OnPlayerScored;
   }

   void OnDisable()
   {
        CountDownText.OnCountdownFinished -= OnCountdownFinished; 
        FB_PlayerMovement.OnPlayerDied -= OnPlayerDied;
        FB_PlayerMovement.OnPlayerScored -= OnPlayerScored;
   }

   void OnCountdownFinished()
   {
       SetPageState(PageState.None);
       OnGameStarted();
       score = 0;
       gameOver = false;
   }

   void OnPlayerDied()
   {
       gameOver = true;
       int savedScore = PlayerPrefs.GetInt("HighScore");
       if (score > savedScore)
       {
           PlayerPrefs.SetInt("HighScore", score);
       }
       SetPageState(PageState.GameOver);
   }


    void OnPlayerScored()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
   void SetPageState(PageState state)
   {
        switch(state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countDownPage.SetActive(false);
                break;
            case PageState.CountDown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(true);
                break;
       }
   }

   public void ConfirmGameOver()
   {
       OnGameOverConfirmed();
       scoreText.text = "Score: 0";
       SetPageState(PageState.Start);
   }

   public void onQuit()
    {
        //go back to menu
        SceneManager.LoadScene(0);
    }

   public void StartGame()
   {
       SetPageState(PageState.CountDown);
   }
}
