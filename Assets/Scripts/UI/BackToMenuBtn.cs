using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuBtn : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
