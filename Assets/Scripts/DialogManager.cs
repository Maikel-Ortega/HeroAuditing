using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextParser textParser;
    public DialogPanel dialogPanel;
    public GameObject interactPrompt;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void LaunchDialog(ScriptableDialog dialog)
    {
        Debug.Log("Launching dialog"+dialog.name);
        dialogPanel.StartDialog(dialog);

    }
    public bool dialogRunning = false;

    public static bool englishText;
    public string GetText(string key)
    {
        return textParser.GetTextByKey(key, englishText);
    }


    public ScriptableDialog testDialog;
    [ContextMenu("TEST_TestDialogStart")]
    public void DoTestDialog()
    {
        LaunchDialog(testDialog);
    }

    public void ShowInteractPrompt(bool show)
    {
        interactPrompt.SetActive(show);
    }

}
