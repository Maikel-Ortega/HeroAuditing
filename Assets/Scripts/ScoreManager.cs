using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    List<ScriptableID> realContradictions;
    List<ScriptableID> markedIDs;
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
        Debug.Log($"Contradictions marked = {markedIDs.Count} \n Real contradictions = {realContradictions.Count} \n Score = {score}");
    }
}
