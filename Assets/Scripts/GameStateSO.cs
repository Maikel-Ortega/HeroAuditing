using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static GameStateSO;

[CreateAssetMenu(fileName = "GameStateSO", menuName = "Scriptable Objects/GameStateSO")]
public class GameStateSO : ScriptableObject
{
    [Serializable]
    public struct BoolTuples
    {
        public string key;
        public bool value;
    }

    [Serializable]
    public class GSConfigData
    {
        public ScriptableID id;
        [SerializeField]
        public List<BoolTuples> data;
    }
    public List<GSConfigData> initialConfig;
}


public class GameState
{
    public Dictionary<ScriptableID, Blackboard> gameState = new Dictionary<ScriptableID, Blackboard>();

    public GameState()
    {

    }

    public GameState(List<GameEntityDataComponent> gameEntities)
    {
        foreach (var entity in gameEntities)
        {
            gameState.Add(entity.id, entity.blackboard);
        }
    }

    public GameState(GameStateSO gameStateSO)
    {
        foreach (var entityData in gameStateSO.initialConfig)
        {
            Blackboard b = new Blackboard();
            foreach (var tuple in entityData.data)
            {
                b.SetBool(tuple.key, tuple.value);
            }

            gameState.Add(entityData.id, b);
        }
    }

    public bool Compare(GameState other, out List<ScriptableID> contradictions)
    {
        bool equals = true;
        contradictions = new List<ScriptableID>();
        foreach (var key in gameState.Keys)
        {
            if(!gameState[key].IsEqual(other.gameState[key]))
            {
                equals = false;
                contradictions.Add(key);
            }
        }        
        return equals;
    }
}