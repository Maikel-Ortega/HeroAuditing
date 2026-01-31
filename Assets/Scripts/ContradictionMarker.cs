using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ContradictionMarker : MonoBehaviour
{
    float range = 3f;
    public LayerMask layerMask;

    public List<IContradictionItem> FindContradictionItemsInRange()
    {
        List<IContradictionItem> contradictionItems = new List<IContradictionItem>();
        List<RaycastHit> hits = new List<RaycastHit>(Physics.SphereCastAll(transform.position, range, transform.forward, 10,layerMask));
        if(hits.Count > 0 )
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponentInParent<IContradictionItem>() != null)
                {
                    contradictionItems.Add(hit.collider.gameObject.GetComponentInParent<IContradictionItem>());
                }

            }
            Debug.Log("CONTRADICTION ITEM FOUND");
        }
        return contradictionItems;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
