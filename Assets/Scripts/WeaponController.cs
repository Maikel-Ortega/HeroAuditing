using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Hitbox weaponHitbox;
    public float anticipationDelay = 0.1f;
    public float hitboxDuration = 0.1f;
    public float recoverySeconds= 0.1f;

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
        isAttacking = true;
        yield return new WaitForSeconds(anticipationDelay);

        float counter = 0;
        weaponHitbox.SetActive(true);


        while (counter < hitboxDuration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        weaponHitbox.SetActive(false);
        yield return new WaitForSeconds(recoverySeconds);

        isAttacking = false;
    }
}
