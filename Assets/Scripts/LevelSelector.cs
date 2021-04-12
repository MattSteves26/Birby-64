using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void Select(int index)
    {
        SceneManager.LoadScene(index);
    }
}
