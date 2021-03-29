using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public Canvas PauseMenuUI;
    public Canvas OptionsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        PauseMenuUI.enabled = false;
        Time.timeScale = 2f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.enabled = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResetGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      PauseMenuUI.enabled = false;
      Time.timeScale = 1f;
      GameIsPaused = false;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
        PauseMenuUI.enabled = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void OptionsMenu()
    {
        PauseMenuUI.enabled = false;
        OptionsMenuUI.enabled = true;
    }

    public void backOptions()
    {
        PauseMenuUI.enabled = true;
        OptionsMenuUI.enabled = false;
    }
}
