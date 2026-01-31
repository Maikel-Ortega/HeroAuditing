using UnityEngine;

public class Enemy : MonoBehaviour, IContradictionItem
{
    public GameEntityDataComponent entityData;


    const string stateKey_Alive = "ALIVE";

    void Awake()
    {
        Init();
    }

    void Init()
    {
        entityData.Init();
        entityData.blackboard.SetBool(stateKey_Alive, true);
    }

    public void OnDeath()
    {
        entityData.blackboard.SetBool(stateKey_Alive, false);        
    }

    public bool IsContradiction(Blackboard data)
    {
        return false;
    }


}
