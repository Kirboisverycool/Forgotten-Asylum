using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Door : MonoBehaviour
{
   

    public bool isInRange;

    [Header("Locked")]
    [SerializeField] bool isLockedDoor;
    [SerializeField] string unlockedItemName;
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] string normalInstructions;
    [SerializeField] string lockedInstructions;
    
    [Header("Scroll Lock")]
    [SerializeField] bool isScrollLocked;
    [SerializeField] GameObject UILock;
    [SerializeField] public List<int> sequenceCode;
    [SerializeField] string ScrollLockedInstructions;

    [Header("Other")]
    [SerializeField] KeyCode keyboardKey;
    [SerializeField] GameObject text;
    [SerializeField] float delayTime;
    [SerializeField] AudioClip doorSound;


    [Header("Identifiers")]
    [SerializeField] string roomNameToGo;
    [SerializeField] public int DoorID;
    [SerializeField] int doorToArrive;
    [SerializeField] public GameObject spawnPoint;
    void Start()
    {
        normalInstructions = instructionText.text;
        if (isLockedDoor)
        {
            instructionText.color = Color.red;
            instructionText.text = lockedInstructions;
        }
        if (isScrollLocked)
        {
            instructionText.color = Color.red;
            instructionText.text = ScrollLockedInstructions;
        }
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(keyboardKey))
        {
            if (!isLockedDoor && !isScrollLocked)
            {

                openDoor();

            }
            else if (isLockedDoor)
            {
                if (FindObjectOfType<InventoryScript>().HasItemInHand() == unlockedItemName)
                {
                    instructionText.color = Color.white;
                    instructionText.text = normalInstructions;
                    isLockedDoor = false;
                }
            }
            else if (isScrollLocked)
            {
              
                var uiLock = Instantiate(UILock, GameObject.FindWithTag("MainCanvas").transform);
                uiLock.GetComponent<ScrollingLock>().correctSequence = sequenceCode;
                uiLock.GetComponent<ScrollingLock>().parentObj = gameObject;
            }   
        }


    }
    public void UnscrollLock()
    {
        isScrollLocked = false;
        instructionText.color = Color.white;
        instructionText.text = normalInstructions;
    }
    public void openDoor()
    {
      
        GameObject.FindWithTag("Fader").GetComponent<Animator>().SetTrigger("Fadeout");
        StartCoroutine(DelayLoad());
    }
    IEnumerator DelayLoad()
    {
        AudioSource.PlayClipAtPoint(doorSound, transform.position);
        yield return new WaitForSeconds(delayTime);
        Rooms roomList =FindObjectOfType<Rooms>();
        for (int i = 0; i < roomList.roomParents.Count; i++)
        {
            if (roomList.roomParents[i].name == roomNameToGo)
            {
                roomList.DisableAll();
                roomList.roomParents[i].SetActive(true);
                roomList.activeIndex = i;
                for (int j = 0; j < roomList.roomParents[i].transform.childCount; j++)
                {
                    if (roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>() != null)
                    {
                        if (roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>().DoorID == doorToArrive)
                        {
                            FindObjectOfType<PlayerMouvement>().transform.position = roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>().spawnPoint.transform.position;
                           // Camera.main.transform.position = roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>().spawnPoint.transform.position;
                        }
                    }
              
                }
            
                yield return null;

            }
        }
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
