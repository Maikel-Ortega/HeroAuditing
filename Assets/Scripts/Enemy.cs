using UnityEngine;

public class Enemy : MonoBehaviour, IContradictionItem
{
    public GameEntityDataComponent entityData;
    public GameObject aliveGraphics;
    public GameObject deadGraphics;

    const string stateKey_Alive = "ALIVE";

    void Awake()
    {
        Init();
    }

    void Init()
    {
        entityData.Init();
        entityData.blackboard.SetBool(stateKey_Alive, true);
        SetGraphics(true);
    }

    public void SetGraphics(bool alive)
    {
        aliveGraphics.SetActive(alive);
        deadGraphics.SetActive(!alive);
    }

    public void OnDeath()
    {
        entityData.blackboard.SetBool(stateKey_Alive, false);
        SetGraphics(false);
    }

    public bool IsContradiction(Blackboard data)
    {
        return false;
    }


}
