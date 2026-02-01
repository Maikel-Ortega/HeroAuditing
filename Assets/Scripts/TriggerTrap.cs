using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTrap : MonoBehaviour, IContradictionItem
{
    public UnityEvent OnTrapTrigger;
    public GameEntityDataComponent entityData;
    public GameObject spikesObject;

    private void Awake()
    {
        entityData.OnAlterCommand += AlterCommand;
        entityData.Init();
        entityData.blackboard.SetBool(stateKey_Trigger, false);
        SetTriggered(false);
    }

    const string stateKey_Trigger = "TRIGGERED";
    void AlterCommand()
    {
        bool currentState = entityData.blackboard.GetBool(stateKey_Trigger);
        SetTriggered(!currentState);
        entityData.blackboard.SetBool(stateKey_Trigger, !currentState);
    }

    void SetTriggered(bool triggered)
    {
        if(triggered)
        {
            spikesObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            spikesObject.transform.localPosition = Vector3.up * -5f;
        }
    }

    void DoTriggerTrap()
    {
        OnTrapTrigger.Invoke();
        SetTriggered(true);
        entityData.blackboard.SetBool(stateKey_Trigger, true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            DoTriggerTrap();            
        }
    }

    public ScriptableID GetId()
    {
        return entityData.id;
    }
}
