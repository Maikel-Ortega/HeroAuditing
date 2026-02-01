using DG.Tweening;
using StarterAssets;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }
    public RectTransform titleTop;
    public RectTransform titleBot;
    public RectTransform missionDescription;


    public RectTransform auditTitleTop;
    public RectTransform auditTitleBot;

    bool interludeFinished;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(ShowMission());
    }

    public void StartAuditorMission()
    {
        StartCoroutine(ShowAuditorMission());
    }


    IEnumerator ShowMission()
    {
        float entrySeconds = 0.35f;
        float entryDelay = 0.5f;
        titleTop.DOAnchorPosY(40, entrySeconds).SetEase(Ease.InCubic).OnComplete(() => titleBot.DOAnchorPosY(440, entrySeconds).SetDelay(entryDelay)).SetDelay(entryDelay);

        
        yield return new WaitForSeconds(2);

        titleTop.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 0.5f);
        titleBot.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 0.5f);

        missionDescription.GetComponentInChildren<TextMeshProUGUI>().DOFade(1f, 0.5f);
        yield return new WaitForSeconds(2);
        missionDescription.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 0.5f);
    }

    IEnumerator ShowAuditorMission()
    {
        float entrySeconds = 0.35f;
        float entryDelay = 0.5f;

        float TopAnchorPosY = 40;
        float BotAnchorPosY = 440;

        auditTitleTop.DOAnchorPosY(TopAnchorPosY, entrySeconds).SetEase(Ease.InCubic).OnComplete(() => auditTitleBot.DOAnchorPosY(BotAnchorPosY, entrySeconds).SetDelay(entryDelay)).SetDelay(entryDelay);


        yield return new WaitForSeconds(2);

        auditTitleTop.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 0.5f);
        auditTitleBot.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 0.5f);

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

        transform.Find("TransitionUI/Document/DocumentTextEs").gameObject.SetActive(false);
        transform.Find("TransitionUI/Document/DocumentTextEn").gameObject.SetActive(false);
        // if(DialogManager.englishText)
            // transform.Find("TransitionUI/Document/DocumentTextEn").gameObject.SetActive(true);
        // else
            // transform.Find("TransitionUI/Document/DocumentTextEs").gameObject.SetActive(true);
    }

    public void EndInterlude()
    {
        // fsm.ChangeState(fsm.owner.st_Auditor);
        GameObject.FindAnyObjectByType<GamePhaseManager>().EndInterludePhase();

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
