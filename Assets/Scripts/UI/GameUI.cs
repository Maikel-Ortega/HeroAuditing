using System.Collections;
using UnityEngine;

public class GameUI : MonoBehaviour
{
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
}
