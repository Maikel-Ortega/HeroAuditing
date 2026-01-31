using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    List<ScriptableID> realContradictions;
    List<ScriptableID> markedIDs;

    public TextMeshProUGUI textMeshProUGUI;
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
            realContradictionsNames += $"Contradiction nº {i+1}:{realContradictions[i]}\n";
        }

        string markedHeader = "MARKED CONTRADICTIONS\n--------------\n";
        for (int i = 0; i < markedIDs.Count; i++)
        {
            markedContradictionsNames += $"Contradiction marked nº {i + 1}:{markedIDs[i]}\n";
        }


        textMeshProUGUI.text = realHeader+ realContradictionsNames + markedHeader + markedContradictionsNames + s;

        Debug.Log(s);

    }
}
