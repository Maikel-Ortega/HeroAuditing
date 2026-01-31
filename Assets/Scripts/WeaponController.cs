using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Hitbox weaponHitbox;
    public float hitboxDuration = 0.1f;
    public bool isAttacking = false;
    private Coroutine attackCoroutine;

    public void Attack()
    {
        if(!isAttacking)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        Debug.Log("START ATTACK COROUTINE");
        isAttacking = true;
        float counter = 0;
        weaponHitbox.SetActive(true);
        while(counter < hitboxDuration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        weaponHitbox.SetActive(false);
        isAttacking = false;

        Debug.Log("END ATTACK COROUTINE");
    }
}
