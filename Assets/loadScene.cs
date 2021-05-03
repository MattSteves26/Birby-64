 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;


 public class loadScene : MonoBehaviour
 {
     public void LoadNewScene()
     {
        //go back to menu
        SceneManager.LoadScene(0);  
     }
 }
 