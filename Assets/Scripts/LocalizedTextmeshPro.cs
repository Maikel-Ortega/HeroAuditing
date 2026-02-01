using TMPro;
using UnityEngine;

public class LocalizedTextmeshPro : MonoBehaviour
{
    public string LocalizationKey;

    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().SetText(DialogManager.Instance.GetText(LocalizationKey));
    }
}
