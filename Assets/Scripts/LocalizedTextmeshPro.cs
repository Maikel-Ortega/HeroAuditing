using TMPro;
using UnityEngine;

public class LocalizedTextmeshPro : MonoBehaviour
{
    public string LocalizationKey;

    private void Start()
    {
        TextMeshProUGUI tmPro = GetComponent<TextMeshProUGUI>();
        string localized = DialogManager.Instance.GetText(LocalizationKey);
        tmPro.SetText(localized);
    }
}
