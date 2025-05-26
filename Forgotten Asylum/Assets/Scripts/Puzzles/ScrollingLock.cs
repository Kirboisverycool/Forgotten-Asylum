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
            Destroy(gameObject);
        }
    }

    public void CloseTab()
    {
        player.GetComponent<PlayerMouvement>().enabled = true;
        Destroy(gameObject);
    }
}
