using UnityEngine;

public class Enemy : MonoBehaviour, IContradictionItem
{
    public GameEntityDataComponent entityData;


    public void OnDeath()
    {
        if (entityData != null) 
        {
        }
    }

    public bool IsContradiction(Blackboard data)
    {
        return false;
    }


}
