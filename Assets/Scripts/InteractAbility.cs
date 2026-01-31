using UnityEngine;

public class InteractAbility : MonoBehaviour
{
    InteractuableMono currentInteractuable;

    private void OnTriggerEnter(Collider other)
    {
        var interactuable =  other.gameObject.GetComponent<InteractuableMono>();
        if (interactuable!= null)
        {
            currentInteractuable = interactuable;
            DialogManager.Instance.ShowInteractPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactuable = other.gameObject.GetComponent<InteractuableMono>();
        if (interactuable == currentInteractuable)
        {
            currentInteractuable = null;

            DialogManager.Instance.ShowInteractPrompt(false);
        }
    }

    public void TryInteract()
    {
        if (currentInteractuable != null)
        {
            currentInteractuable.Interact();
        }
    }
}
