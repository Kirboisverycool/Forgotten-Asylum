using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
   

    public bool isInRange;

    [Header("Locked")]
    [SerializeField] bool isLockedDoor;
    [SerializeField] string unlockedItemName;
    
    [Header("Other")]
    [SerializeField] KeyCode keyboardKey;
    [SerializeField] GameObject text;
    [SerializeField] float delayTime;

    [Header("Identifiers")]
    [SerializeField] string roomNameToGo;
    [SerializeField] public int DoorID;
    [SerializeField] int doorToArrive;
    [SerializeField] public GameObject spawnPoint;
    void Start()
    {
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(keyboardKey))
        {
            if (!isLockedDoor)
            {
                openDoor();
            
            }
            else 
            {
                if(FindObjectOfType<InventoryScript>().HasItemInHand() == unlockedItemName)
                {
                    openDoor();
                }
            }
        }


    }
    private void openDoor()
    {
        FindObjectOfType<SpawnPoints>().SetNextPoint(doorToArrive);
        GameObject.FindWithTag("Fader").GetComponent<Animator>().SetTrigger("Fadeout");
        StartCoroutine(DelayLoad());
    }
    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(roomNameToGo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            isInRange = true;
            text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            text.SetActive(false);
        }
    
    }
}
