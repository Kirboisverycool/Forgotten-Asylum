using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] Door[] doors;

    // Start is called before the first frame update
    void Start()
    {
        doors = Resources.FindObjectsOfTypeAll<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            if (doors[i].canUse == false)
            {
                doors[i].canUse = false;
                break;
            }
            else if (doors[i].canUse == true)
            {
                doors[i].canUse = true;
                break;
            }
        }
    }
}
