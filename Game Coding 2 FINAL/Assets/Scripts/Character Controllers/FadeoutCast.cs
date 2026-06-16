using System;
using System.Collections.Generic;
using UnityEngine;

public class FadeoutCast : MonoBehaviour
{
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private Transform targetTransform;
    private List<GameObject> objectsInRow = new List<GameObject>();

    [SerializeField] private float fadeTime;
    void LateUpdate()
    {
        Ray currentRay = new Ray(transform.position, (targetTransform.position - transform.position).normalized);
        RaycastHit[] RayData = Physics.RaycastAll(currentRay, (targetTransform.position - transform.position).magnitude, objectLayer);

        for(int i = 0; i < RayData.Length; i++)
        {
            if (RayData[i].transform.GetComponent<FadeOut>() != null && !CheckObstacles(RayData[i].transform.gameObject))
            {
                objectsInRow.Add(RayData[i].transform.gameObject);
                RayData[i].transform.GetComponent<FadeOut>().StartFadeOut(fadeTime);
            }
        }

        for(int i = 0; i < objectsInRow.Count; i++)
        {
            if (!CheckRaycast(objectsInRow[i], RayData)){
                objectsInRow[i].GetComponent<FadeOut>().StartFadeIn(fadeTime);
                objectsInRow.RemoveAt(i);
            }
        }

    }
    public void RemoveObstacle(GameObject obstacle)
    {
        if (objectsInRow.Contains(obstacle))
        {
            objectsInRow.Remove(obstacle);
        }
    }
    private bool CheckObstacles(GameObject currentSelected)
    {
        for (int i = 0; i < objectsInRow.Count; i++)
        {
            if (objectsInRow[i] == currentSelected)
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckRaycast(GameObject currentSelected, RaycastHit[] rayData)
    {
        for (int i = 0; i < rayData.Length; i++)
        {
            if(currentSelected == rayData[i].transform.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
