using UnityEngine;
using UnityEngine.Events;

public class InteractuableMono : MonoBehaviour
{
    public UnityEvent OnInteract;
    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
