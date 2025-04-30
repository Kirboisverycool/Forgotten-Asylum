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
    [SerializeField] List<int> correctSequence;
    public List<int> sequence;
    public List<TextMeshProUGUI> numText;
    AudioSource audioS;
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
       
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
