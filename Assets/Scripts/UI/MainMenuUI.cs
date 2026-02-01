using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public string introEs;
    public string introEn;

    public void GoToIntro()
    {
        transform.Find("MainMenu").gameObject.SetActive(false);
        transform.Find("Intro").gameObject.SetActive(true);


        transform.Find("Intro/IntroText").GetComponent<TMP_Text>().text = introEn;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
