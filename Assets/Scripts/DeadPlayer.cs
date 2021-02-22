using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas dp;
    public Button restart;
    public Button quit;
   
    public void onRestart()
    {
        //Reset Everything back to the start
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        dp.enabled = false;
    }

    public void onQuit()
    {
        //go back to menu
        SceneManager.LoadScene(0);
    }
}
