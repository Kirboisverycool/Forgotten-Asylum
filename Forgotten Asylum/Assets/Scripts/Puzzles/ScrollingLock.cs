using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScrollingLock : MonoBehaviour
{
    [SerializeField] Sprite unlockedSprite;
    [SerializeField] AudioClip clickAudio;
    [SerializeField] AudioClip correctAudio;
    [SerializeField] public List<int> correctSequence;
    public List<int> sequence;
    public List<TextMeshProUGUI> numText;
    AudioSource audioS;
    GameObject player;

    public GameObject parentObj;


    [SerializeField] float test;
    [SerializeField] Vector3 startingMousePosition;
    [SerializeField] Vector3 currentMousePosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<PlayerMouvement>().enabled = false;
    }

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        for (int i = 0; i < transform.GetChild(0).childCount ; i++)
        {
            numText.Add(transform.GetChild(0).GetChild(i).GetComponentInChildren<TextMeshProUGUI>());
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startingMousePosition.x = Input.mousePosition.x;
        }
    }

    public void GetMousePosition()
    {
        if (Input.GetMouseButton(0))
        {
            currentMousePosition.x = Input.mousePosition.x;

            test = currentMousePosition.x - startingMousePosition.x;
        }
    }

    public void CheckIfLeftOrRight(int index)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (test >= 0)
            {
                UpdateNumber(index);
            }
            if (test < 0)
            {
                UpdateNegativeNumber(index);
            }
        }
    }

    public void UpdateNumber(int index)
    {
        audioS.PlayOneShot(clickAudio);
        if (sequence[index] + 1 == 10)
        {
            sequence[index] = 0;
        }
        else
        {
            sequence[index]++;
        }
       
        updateNumText(index, sequence[index]);
        StartCoroutine(Compare());
    }

    public void UpdateNegativeNumber(int index)
    {
        audioS.PlayOneShot(clickAudio);
        if (sequence[index] - 1 == -1)
        {
            sequence[index] = 9;
        }
        else
        {
            sequence[index]--;
        }
        updateNumText(index, sequence[index]);
        StartCoroutine(Compare());
    }

   //Check
    private void updateNumText(int index, int number)
    {
        numText[index].text = number.ToString();
    }
    private IEnumerator Compare()
    {
        int isCorrect = 0;

        for (int i = 0; i < sequence.Count; i++)
        {
            if (sequence[i] == correctSequence[i])
            {
                isCorrect++;
            }
        }
        
        if (isCorrect == 4)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = unlockedSprite;
            audioS.PlayOneShot(correctAudio);
            Debug.Log("Correct");
            yield return new WaitForSeconds(2);
            parentObj.GetComponent<Door>().UnscrollLock();
            player.GetComponent<PlayerMouvement>().enabled = true;
            Destroy(gameObject);
        }
    }

    public void CloseTab()
    {
        player.GetComponent<PlayerMouvement>().enabled = true;
        Destroy(gameObject);
    }
}
