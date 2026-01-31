using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public void OnPlayerDamaged()
    {
    }

    public void  OnPlayerDeath()
    {
        SceneManager.LoadScene(1);
    }

}
