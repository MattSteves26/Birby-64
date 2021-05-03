using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Canvas EndLevelUI;

   
    public void onQuit()
    {
        //go back to menu
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {

        Debug.Log("NEXT LEVEL");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
