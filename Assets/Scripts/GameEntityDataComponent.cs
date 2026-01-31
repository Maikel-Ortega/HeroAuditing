using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Blackboard
{
    public Blackboard()
    {
        
    }

    public Dictionary<string, bool> boolState = new Dictionary<string, bool>();
    public bool IsEqual(Blackboard other)
    {
        foreach (var item in boolState.Keys)
        {
            if(other.boolState[item] != boolState[item])
            {
                return false;
            }
        }
        return true;
    }

    public void SetBool(string key, bool value) 
    {
        boolState[key] = value;
        Debug.Log($"Object = {this}. SET BOOL: {key} = {value}");
    }

    public override string ToString()
    {
        string s = "";
        foreach (var item in boolState.Keys)
        {
            s+= $"({item}:{boolState [item]})\n";
        }
        return s;
    }
}

public class GameEntityDataComponent : MonoBehaviour
{
    public Blackboard blackboard;
    public ScriptableID id;
    public void Init()
    {
        blackboard = new Blackboard();
    }

    [ContextMenu("Debug_LogBlackboard")]
    public void DEBUG_LogBlackboard()
    {
        Debug.Log(blackboard.ToString());
    }
}
