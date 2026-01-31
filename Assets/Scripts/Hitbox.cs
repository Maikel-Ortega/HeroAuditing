using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DamageData
{
    public int dmgAmount;
}

public class Hitbox : MonoBehaviour
{
    public DamageData damageData;
    public bool drawDebug = true;
    private BoxCollider hitboxCollider;

    private void Awake()
    {
        hitboxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        List<Hurtbox> hurtBoxes = new List<Hurtbox>(other.GetComponents<Hurtbox>());
        if(hurtBoxes.Count > 0 )
        {
            foreach (var item in hurtBoxes)
            {
                Debug.Log($"Hitbox hits: Hurtbox={item}");
                if (item.transform.root.gameObject != transform.root.gameObject) // cant hurt itself
                    item.damageable.Damage(damageData.dmgAmount);
            }
        }
    }

    public bool IsActive()
    {
        return hitboxCollider.enabled;
    }

    public void SetActive(bool active)
    {
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = active;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawDebug && hitboxCollider != null && hitboxCollider.enabled)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(hitboxCollider.center, hitboxCollider.size);
        }
    }
}