using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ContradictionMarker : MonoBehaviour
{
    float range = 3f;
    public LayerMask layerMask;
    public TextMeshPro numberFace1;
    public TextMeshPro numberFace2;


    public void SetNumber(int number)
    {
        numberFace1.text = number.ToString();
        numberFace2.text = number.ToString();
    }

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
                    var item = hit.collider.gameObject.GetComponentInParent<IContradictionItem>();
                    if(!contradictionItems.Contains(item) )
                    {
                        contradictionItems.Add(item);
                    }
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
