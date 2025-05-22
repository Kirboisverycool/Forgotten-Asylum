using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    [SerializeField] public List<GameObject> roomParents;

    public GameObject roomPresent;
    public GameObject roomPast;

    public int activeIndex;
    private void Start()
    {
        for (int i = 0; i < roomParents.Count; i++)
        {
            if (roomParents[i] == isActiveAndEnabled)
            {
                i = activeIndex;
                FindPresentAndPast();
                return;
            }

          
        }
    }
    private void FindPresentAndPast()
    {
       roomPresent = roomParents[activeIndex].transform.Find("Current").gameObject;
       roomPast = roomParents[activeIndex].transform.Find("Past").gameObject;
        FlashBackManager fbm = FindObjectOfType<FlashBackManager>();
         fbm.presentTimeScene = roomPresent;
         fbm.pastTimeScene = roomPast;
        fbm.EnsureCorrectScene();
    }
    public void SetNewActive(int index)
    {
        DisableAll();
        roomParents[index].SetActive(true);
        activeIndex = index;
        FindPresentAndPast();
       
    }
    public void DisableAll()
    {
        for (int i = 0; i < roomParents.Count; i++)
        {
            roomParents[i].SetActive(false);
        }
    }
}
