using DG.Tweening;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    public void DoOnDamage()
    {
        transform.localPosition = Vector3.zero;        
        transform.DOPunchScale(Vector3.one * 0.5f, 0.2f);
        transform.DOPunchPosition(Vector3.up* 0.5f, 0.35f,4);

    }
}
