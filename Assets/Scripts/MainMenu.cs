using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Canvas mainmenu;
    public Canvas LevelSection;
    public Canvas Options;

    private static int levelmenu_switch = 1;
    private static int optionmenu_switch = 1;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void LevelSelection()
    {
        if (levelmenu_switch == 1)
        {
            mainmenu.enabled = false;
            LevelSection.enabled = true;
            levelmenu_switch = 0;
        }
        else
        {
            mainmenu.enabled = true;
            LevelSection.enabled = false;
            levelmenu_switch = 1;
        }
    }

    public void OptionsMenu()
    {
        if(optionmenu_switch == 1)
        {
            mainmenu.enabled = false;
            Options.enabled = true;
            optionmenu_switch = 0;
        }
        else
        {
            mainmenu.enabled = true;
            Options.enabled = false;
            optionmenu_switch = 1;
        }
    }

    public void Credits()
    {
        SceneManager.LoadScene(7);
    }
}
