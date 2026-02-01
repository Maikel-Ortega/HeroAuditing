using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ContradictionAbility : MonoBehaviour
{
    public Transform rayOrigin;

    public LayerMask contradictionMarkerMask;
    public LayerMask deployablePlacesMask;

    public GameObject contradictionMarkerPrefab;
    
    public List<GameObject> deployedMarkers = new List<GameObject>();

    private void Start()
    {
        deployedMarkers = new List<GameObject>();
    }

    public void Execute()
    {
        //Launch raycast
        RaycastHit hitInfo;
        Color debugColor = Color.green;
        if(Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hitInfo, 9999,contradictionMarkerMask))
        {
            //If hits ContradictionMarker, remove it. 
            DeleteMarker(hitInfo.collider.gameObject);
        }        
        else
        {
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hitInfo, 9999, deployablePlacesMask))
            {
                //If not, if hits good surface, deploy one. 
                DeployMarker(hitInfo.point, hitInfo.normal);
                debugColor = Color.blue;
            }
            else
            {
                debugColor = Color.red;
            }
        }
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward, debugColor, 1f);

    }


    public void DeployMarker(Vector3 pos, Vector3 normal)
    {
        GameObject go = Instantiate(contradictionMarkerPrefab,pos, Quaternion.identity);
        deployedMarkers.Add(go);
    }

    public void DeleteMarker(GameObject go)
    {
        deployedMarkers.Remove(go);
        Destroy(go);
    }

    public List<IContradictionItem> GetAllContradictionItems()
    {
        List<IContradictionItem> allItemsFound = new List<IContradictionItem>();
        foreach (var item in deployedMarkers)
        {
            List<IContradictionItem> foundItems =  item.GetComponent<ContradictionMarker>().FindContradictionItemsInRange();
            allItemsFound.AddRange(foundItems);
        }
        Debug.Log($"Number of contradiction items in range of markers=  {allItemsFound.Count}");
        return allItemsFound;
    }
}

