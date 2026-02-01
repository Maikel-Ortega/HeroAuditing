using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogPanel : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;


    public void StartDialog(ScriptableDialog dialogAsset)
    {
        if(!DialogManager.Instance.dialogRunning)
        {
            StartCoroutine(DialogCouroutine(dialogAsset));
        }
    }

    string lastLine = "";
    void UpdateText(string key)
    {
        AudioManager.StopOneshot();
        AudioManager.Play(key,false,1f);
        lastLine = key;
        textMeshProUGUI.text = DialogManager.Instance.GetText(key);
    }


    void OnDialogStart()
    {
        DialogManager.Instance.dialogRunning = true;

        this.GetComponent<RectTransform>().DOAnchorPosY(65f, 0.5f).SetEase(Ease.OutQuad);
    }

    void OnDialogEnds()
    {

        DialogManager.Instance.dialogRunning = false;
        this.GetComponent<RectTransform>().DOAnchorPosY(-385.96f, 0.5f).SetEase(Ease.OutQuad);

    }

    public IEnumerator DialogCouroutine(ScriptableDialog dialogAsset)
    {
        int i = 0;
        OnDialogStart();
        bool waiting = true;
        while(i<dialogAsset.dialogIDs.Count)
        {
            UpdateText(dialogAsset.dialogIDs[i]);
            while(waiting)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    waiting = false;
                }
                yield return null;
            }
            waiting = true;
            i++;
        }
        AudioManager.Stop(lastLine);

        OnDialogEnds();
    }

}
