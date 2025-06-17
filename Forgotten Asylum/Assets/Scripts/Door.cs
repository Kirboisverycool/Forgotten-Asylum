using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class Door : MonoBehaviour
{
    public bool isInRange;

    [Header("Locked")]
    [SerializeField] bool isLockedDoor;
    [SerializeField] string unlockedItemName;
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] string normalInstructions;
    [SerializeField] string lockedInstructions;
    [SerializeField] AudioClip unlockAudio;

    [Header("Scroll Lock")]
    [SerializeField] bool isScrollLocked;
    [SerializeField] GameObject UILock;
    [SerializeField] public List<int> sequenceCode;
    [SerializeField] string scrollLockedInstructions;
    RandomNumberManager randomNumberManager;

    [Header("Paint Scroll Lock")]
    [SerializeField] bool isPaintScrollLocked;
    [SerializeField] GameObject paintUILock;
    [SerializeField] public List<int> paintSequenceCode;
    [SerializeField] string paintScrollLockedInstructions;

    [Header("Past Door")]
    [SerializeField] bool isPastDoor = false;
   

    [Header("Chase End Door")]
    [SerializeField] bool isChaseEndDoor = false;
    public GameObject enemyAI;

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
  
    public bool canUse = true;
    public bool isChasing = true;

    private void Awake()
    {
        randomNumberManager = FindObjectOfType<RandomNumberManager>();
    }

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
            instructionText.text = scrollLockedInstructions;
            for (int i = 0; i < sequenceCode.Count; i++)
            {
                sequenceCode[i] = randomNumberManager.scrollingLockCode[i];
            }
        }
        if (isPaintScrollLocked)
        {
            instructionText.color = Color.red;
            instructionText.text = paintScrollLockedInstructions;
        }
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyAI == null)
        {
            enemyAI = GameObject.FindGameObjectWithTag("Enemy");
        }

        if (isPastDoor)
        {
            PastDoor();
            isLockedDoor = !FindObjectOfType<FlashBackManager>().isInPast;
        }


        if (isInRange && Input.GetKeyDown(keyboardKey))
        {
            if (!isLockedDoor && !isScrollLocked && !isPaintScrollLocked)
            {
                if (canUse)
                {
                    OpenDoor();
                }
            }
            else if (isLockedDoor)
            {
                if (FindObjectOfType<InventoryScript>().HasItemInHand() == unlockedItemName)
                {
                    FindAnyObjectByType<InventoryScript>().RemoveFromInventory();
                    instructionText.color = Color.white;
                    instructionText.text = normalInstructions;
                    AudioSource.PlayClipAtPoint(unlockAudio, transform.position);
                    isLockedDoor = false;
                }
            }
            else if (isScrollLocked)
            {
                var uiLock = Instantiate(UILock, GameObject.FindWithTag("MainCanvas").transform);
                uiLock.GetComponent<ScrollingLock>().correctSequence = sequenceCode;
                uiLock.GetComponent<ScrollingLock>().parentObj = gameObject;
            }
            else if (isPaintScrollLocked)
            {
                var paintuiLock = Instantiate(paintUILock, GameObject.FindWithTag("MainCanvas").transform);
                paintuiLock.GetComponent<ScrollingLock>().correctSequence = paintSequenceCode;
                paintuiLock.GetComponent<ScrollingLock>().parentObj = gameObject;
            }
        }
    }

    private void PastDoor()
    {
        if (isPastDoor && !isLockedDoor)
        {
            instructionText.color = Color.white;
            instructionText.text = normalInstructions;
        }
        else if (isPastDoor && isLockedDoor)
        {
            instructionText.color = Color.red;
            instructionText.text = lockedInstructions;
        }
    }


    public void UnscrollLock()
    {
        isScrollLocked = false;
        isPaintScrollLocked = false;
        instructionText.color = Color.white;
        instructionText.text = normalInstructions;
    }
    public void OpenDoor()
    {
        canUse = false;
        GameObject.FindWithTag("Fader").GetComponent<Animator>().SetTrigger("Fadeout");
        StartCoroutine(DelayLoad());
    }
    IEnumerator DelayLoad()
    {
        AudioSource.PlayClipAtPoint(doorSound, transform.position);
        GameObject.FindGameObjectWithTag("VirtualCamera").transform.GetChild(0).gameObject.SetActive(false);
        if(enemyAI != null)
        {
            enemyAI.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            enemyAI.GetComponent<EnemyAI>().enabled = false;
        }
        yield return new WaitForSeconds(delayTime);
        Rooms roomList = FindObjectOfType<Rooms>();
        for (int i = 0; i < roomList.roomParents.Count; i++)
        {
            if (roomList.roomParents[i].name == roomNameToGo)
            {
                roomList.SetNewActive(i);
                for (int j = 0; j < roomList.roomParents[i].transform.childCount; j++)
                {
                    if (roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>() != null)
                    {
                        if (roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>().DoorID == doorToArrive)
                        {
                            FindObjectOfType<PlayerMouvement>().transform.position = roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>().spawnPoint.transform.position;
                            roomList.roomParents[i].transform.GetChild(j).GetComponent<Door>().StartDelayRoutine();
                            GameObject.FindGameObjectWithTag("VirtualCamera").transform.GetChild(0).gameObject.SetActive(true);
                            if (enemyAI != null && !isChaseEndDoor)
                            {
                                enemyAI.GetComponent<EnemyAI>().enabled = true;
                            }
                            else if(enemyAI != null && isChaseEndDoor)
                            {
                                enemyAI.SetActive(false);
                            }
                            canUse = true;
                        }
                    }
                }
            
                yield return null;
            }
        }
    }
    public void StartDelayRoutine()
    {
        StartCoroutine(LoadCoolDown());
    }
    IEnumerator LoadCoolDown()
    {
        Debug.Log(gameObject);
        Debug.Log(" coolDown");
        canUse = false;
        yield return new WaitForSeconds(2);
        canUse = true;
        Debug.Log(" coolDownOver");
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
