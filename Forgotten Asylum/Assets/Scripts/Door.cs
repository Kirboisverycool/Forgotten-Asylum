using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
   
    bool isLockedDoor;
    public bool isInRange;
    [SerializeField] string roomName;
    [SerializeField] string unlockedItemName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLockedDoor)
            {
                SceneManager.LoadScene(roomName);
            }
            else 
            {
                if(FindObjectOfType<InventoryScript>().HasItemInHand() == unlockedItemName)
                {
                    SceneManager.LoadScene(roomName);
                }
            }
        }


    }
    private void openDoor()
    { 
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }
}
