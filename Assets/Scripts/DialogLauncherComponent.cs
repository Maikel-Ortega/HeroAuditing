using UnityEngine;

public class DialogLauncherComponent : MonoBehaviour
{
    public ScriptableDialog dialogReference;
    public void DoLaunchDialog()
    {
        DialogManager.Instance.LaunchDialog(dialogReference);
    }

}
