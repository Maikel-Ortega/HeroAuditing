using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public List<GameEntityDataComponent> gameEntities;

    public GameStateSO testGameStateSO;

    private void Awake()
    {
        gameEntities = new List<GameEntityDataComponent>(FindObjectsByType<GameEntityDataComponent>(FindObjectsInactive.Include,FindObjectsSortMode.None));        
            
    
    }

    [ContextMenu("TEST_CheckContradiction")]
    public void TEST_CheckContradiction()
    {
        GameState currentGameState = new GameState(gameEntities);
        GameState testGS = new GameState(testGameStateSO);

        List<ScriptableID> contradictions = new List<ScriptableID>();
        bool result = currentGameState.Compare(testGS,out contradictions);

        if (!result)
        {
            Debug.Log($"CONTRADICTIONS FOUND: {contradictions.Count}");
            foreach (var item in contradictions)
            {
                Debug.Log(item.ToString());
            }
        }
        else
        {
            Debug.Log("All good, no contradictions");
        }

    }
}
