using StarterAssets;
using System;
using System.Collections;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    bool interludeFinished;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // StartCoroutine(ShowMission());
    }

    IEnumerator ShowMission()
    {
        transform.Find("Mission").gameObject.SetActive(true);
        transform.Find("DialogueBox").gameObject.SetActive(false);
        
        yield return new WaitForSeconds(4);

        transform.Find("Mission").gameObject.SetActive(false);
    }

    IEnumerator ShowTransition()
    {
        transform.Find("TransitionUI").gameObject.SetActive(true);

        transform.Find("Mission").gameObject.SetActive(false);
        transform.Find("DialogueBox").gameObject.SetActive(false);

        yield return null;
    }

    public void ShowInterludeUI()
    {
        transform.Find("TransitionUI").gameObject.SetActive(true);
        transform.Find("TransitionUI/Intermission").gameObject.SetActive(true);
        transform.Find("TransitionUI/Document").gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowDocumentUI()
    {
        transform.Find("TransitionUI").gameObject.SetActive(true);
        transform.Find("TransitionUI/Intermission").gameObject.SetActive(false);
        transform.Find("TransitionUI/Document").gameObject.SetActive(true);
    }

    public void EndInterlude()
    {
        transform.Find("TransitionUI").gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;

        interludeFinished = true;

        FindFirstObjectByType<StarterAssetsInputs>().ClearInputs();
    }

    public bool HasInterludeEnded()
    {
        return interludeFinished;
    }

    public void AuditorUI()
    {
        transform.Find("HeroGUI").gameObject.SetActive(false);
        transform.Find("AuditorGUI").gameObject.SetActive(true);
    }
}
