using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public int nextPoint;
  
    private void Awake()
    {
        int numberOf = FindObjectsOfType<SpawnPoints>().Length;
        if (numberOf > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);

         
        }
       


    }
  
    public void SetNextPoint(int location)
    {
        nextPoint = location;
    }
    
    public void FindSpawnPoint()
    {

        Door[] doorLists = FindObjectsOfType<Door>();
        Debug.Log(doorLists.Length);

        for (int i = 0; i <FindObjectsOfType<Door>().Length; i++)
        {
            if (doorLists[i].DoorID == nextPoint)
            {
                FindObjectOfType<PlayerMouvement>().transform.position = doorLists[i].spawnPoint.transform.position;

            }
            
          
        }
    }

    
}
