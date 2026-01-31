using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
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
}

public class GameEntityDataComponent : MonoBehaviour
{
    public Blackboard blackboard;
        
    private void Awake()
    {
        blackboard = GetComponent<Blackboard>();
    }    
}
