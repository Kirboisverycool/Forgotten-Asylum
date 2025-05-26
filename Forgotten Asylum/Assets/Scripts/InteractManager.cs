using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public List<GameObject> interactables;

    void Update()
    {
        DecideNearestInteractable();
    }

    public void AddToList(GameObject obj)
    { 
        interactables.Add(obj);
    }
    public void RemoveFromList(GameObject obj)
    {
        interactables.Remove(obj);    
    }
    private void DecideNearestInteractable()
    {
        GameObject nearestObject= null;
        float nearestDistance = Mathf.Infinity;
        for (int i = 0; i < interactables.Count; i++)
        {
            float distance = Vector2.Distance(interactables[i].gameObject.transform.position, transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = interactables[i];
            }
        }
        if (nearestObject != null)
        {
            nearestObject.GetComponent<Interactables>().IsNearest();
            for (int i = 0; i < interactables.Count; i++)
            {
                if (interactables[i] != nearestObject)
                {
                    interactables[i].GetComponent<Interactables>().NotNearest();
                }

            }
        }

    }
}
