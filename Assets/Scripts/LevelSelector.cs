using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private void Start()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
          foreach (Button but in buttons)
             Debug.Log(but);
         if(LevelUnLock.unlockLevel < 8)
        {
            buttons[LevelUnLock.unlockLevel].interactable = true;
        }
    }
    public void Select(int index)
    {
        SceneManager.LoadScene(index);
    }
}
