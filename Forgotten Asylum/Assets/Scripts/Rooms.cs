using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    [SerializeField] public List<GameObject> roomParents;
    public int activeIndex;
    private void Start()
    {
        for (int i = 0; i < roomParents.Count; i++)
        {
            if (roomParents[i] == isActiveAndEnabled)
            {
                i = activeIndex;
                return;
            }

          
        }
    }
    public void DisableAll()
    {
        for (int i = 0; i < roomParents.Count; i++)
        {
            roomParents[i].SetActive(false);
        }
    }
}
