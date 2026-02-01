using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public string introEs;
    public string introEn;

    void Start()
    {
        AudioManager.Stop("Auditor");
        AudioManager.Stop("HERO");
        AudioManager.Stop("Menu");
        AudioManager.Stop("Puntuacion");

        AudioManager.Play("Menu", true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoToIntro()
    {
        transform.Find("MainMenu").gameObject.SetActive(false);
        transform.Find("Intro").gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetSpanish()
    {
        transform.Find("MainMenu/Button/Text").GetComponent<TMP_Text>().text = "JUGAR";
        transform.Find("Intro/IntroText").GetComponent<TMP_Text>().text = introEs;
        transform.Find("Intro/IntroButton/IntroButtonTxt").GetComponent<TMP_Text>().text = "SIGUIENTE";
        DialogManager.englishText = false;
    }

    public void SetEnglish()
    {
        transform.Find("MainMenu/Button/Text").GetComponent<TMP_Text>().text = "PLAY";
        transform.Find("Intro/IntroText").GetComponent<TMP_Text>().text = introEn;
        transform.Find("Intro/IntroButton/IntroButtonTxt").GetComponent<TMP_Text>().text = "NEXT";
        DialogManager.englishText = true;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb.escapeKey.wasPressedThisFrame)
            Application.Quit();
    }
}
