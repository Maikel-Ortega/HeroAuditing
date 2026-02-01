using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTrap : MonoBehaviour
{
    public UnityEvent OnTrapTrigger;
    public GameEntityDataComponent entityData;

    private void Awake()
    {
        entityData.OnAlterCommand += AlterCommand;

    }
    const string stateKey_Trigger = "TRIGGERED";
    void AlterCommand()
    {
        bool currentState = entityData.blackboard.GetBool(stateKey_Trigger);
        SetTriggered(!currentState);
        entityData.blackboard.SetBool(stateKey_Trigger, !currentState);
    }

    void SetTriggered(bool st)
    {

    }

    void DoTriggerTrap()
    {
        OnTrapTrigger.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        DoTriggerTrap();
    }
}
