using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    List<ScriptableID> realContradictions;
    List<ScriptableID> markedIDs;

    public TextMeshProUGUI markedContradictionsText;
    public TextMeshProUGUI realContradictionsText;
    public TextMeshProUGUI scoreText;

    public string colorCodeRight = "#00ff00ff";

    public string colorCodeWrong = "#FF0000ff";

    int score = 0;
    public void CalculateScore()
    {
        var contradictionAbility = FindAnyObjectByType<ContradictionAbility>();
        List<IContradictionItem> markedItems = contradictionAbility.GetAllContradictionItems();
        var mgr = FindFirstObjectByType<GamePhaseManager>();
        realContradictions = mgr.GetRealContradictions();
        markedIDs = new List<ScriptableID>();
        foreach (var item in markedItems)
        {
            markedIDs.Add(item.GetId());
        }

       
        foreach (var item in realContradictions)
        {
            if (markedIDs.Contains(item))
            {
                Debug.Log("CONTRADICTION WAS MARKED BY THE PLAYER!");
            }
        }

        foreach(var item in markedIDs)
        {
            if(realContradictions.Contains(item))
            {
                score += 1;
            }
            else
            {
                score -= 1;
            }
        }
        string s = $"Contradictions marked = {markedIDs.Count} \n Real contradictions = {realContradictions.Count} \n Score = {score}";

        string realContradictionsNames="";
        string markedContradictionsNames = "";

        var gsm = FindFirstObjectByType<GameStateManager>();

        string realHeader = "REAL CONTRADICTIONS\n--------------\n";
        for (int i = 0; i < realContradictions.Count;i++)
        {

            bool wasMarked = markedIDs.Contains(realContradictions[i]);

            string colorString = wasMarked ? $"<color={colorCodeRight}>" : $"<color={colorCodeWrong}>";
            realContradictionsNames += $"{colorString}  Contradiction nº {i+1}:{realContradictions[i]} </color>\n";
        }

        string markedHeader = "MARKED CONTRADICTIONS\n--------------\n";
        for (int i = 0; i < markedIDs.Count; i++)
        {
            markedContradictionsNames += $"Contradiction marked nº {i + 1}:{markedIDs[i]}\n";
        }

        markedContradictionsText.text = markedHeader + markedContradictionsNames;
        realContradictionsText.text = realHeader + realContradictionsNames;

        scoreText.text = "SCORE: " + score;

        Debug.Log(s);


        StartCoroutine(ScoreAnimation());
    }

    void AppearTextAnimation(TextMeshProUGUI t)
    {
        t.DOFade(1, 0.1f);
        t.rectTransform.DOScale(Vector3.one * 1, 0.5f);
    }

    IEnumerator ScoreAnimation()
    {
        AppearTextAnimation(markedContradictionsText);
        yield return new WaitForSeconds(1f);


        AppearTextAnimation(realContradictionsText);
        yield return new WaitForSeconds(1f);


        AppearTextAnimation(scoreText);
        yield return new WaitForSeconds(1f);
    }
}
