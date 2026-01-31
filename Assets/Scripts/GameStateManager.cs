using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public List<GameEntityDataComponent> gameEntities;

    public GameStateSO testGameStateSO;

    private void Awake()
    {
        gameEntities = new List<GameEntityDataComponent>(FindObjectsByType<GameEntityDataComponent>(FindObjectsInactive.Include,FindObjectsSortMode.None));        
            
    
    }

    public GameEntityDataComponent FindById(ScriptableID id)
    {
        var found =  gameEntities.Find(x => x.id == id);
        return found;
    }

    public void MakeRandomChanges(int n)
    {
        List<GameEntityDataComponent> changedEntities = new List<GameEntityDataComponent>();
        for (int i = 0; i < n; i++)
        {
            if (i < gameEntities.Count)
            {
                var entity = gameEntities[Random.Range(0, n)];
                if(!changedEntities.Contains(entity))
                {
                    AlterEntity(entity);
                    changedEntities.Add(entity);
                }
            }
        }
    }

    public void AlterEntity(GameEntityDataComponent entity)
    {
        entity.AlterCurrentState();
    }

    public GameState savedGameState;
    public void SaveGameState()
    {
        savedGameState = new GameState(gameEntities);
    }

    public List<ScriptableID> GetDifferences(GameState gsA, GameState gsB)
    {
        List<ScriptableID> contradictions = new List<ScriptableID>();
        bool result = gsA.Compare(gsB, out contradictions);
        return contradictions;
    }

    public List<ScriptableID> GetDifferencesFromSavedState()
    {
        var currentState = new GameState(gameEntities);
        return GetDifferences(savedGameState, currentState);
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
