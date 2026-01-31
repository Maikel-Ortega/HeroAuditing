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

    public Blackboard GetCopy()
    {
        var newCopy = new Blackboard();
        foreach (var item in boolState.Keys)
        {
           newCopy.boolState[item] = boolState[item];
        }
        return newCopy;
    }

    public void SetBool(string key, bool value) 
    {
        boolState[key] = value;
        Debug.Log($"Object = {this}. SET BOOL: {key} = {value}");
    }

    public bool GetBool(string key)
    {
        return boolState[key];
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
    public System.Action OnAlterCommand;
    public Blackboard blackboard;
    public ScriptableID id;
    public void Init()
    {
        blackboard = new Blackboard();
    }

    public void AlterCurrentState()
    {
        OnAlterCommand.Invoke();
    }

    [ContextMenu("Debug_LogBlackboard")]
    public void DEBUG_LogBlackboard()
    {
        Debug.Log(blackboard.ToString());
    }
}
