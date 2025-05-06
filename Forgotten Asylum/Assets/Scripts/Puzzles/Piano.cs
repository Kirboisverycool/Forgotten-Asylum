using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Piano : MonoBehaviour
{
    [Header("Code will be around 7 digits")]
    [SerializeField] List<int> correctCode = new List<int>();
    [SerializeField] int codeLength = 7;
    [SerializeField] int currentNumber = -1;
    [SerializeField] List<int> codeText = new List<int>();
    [SerializeField] int finalDigit;

    [SerializeField] bool finishedPuzzle = false;
    /*    [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip audioClip;*/

    public void PressButton(int num)
    {
        if(!finishedPuzzle)
        {
            if (codeText.Count < codeLength)
            {
                codeText.Add(num);
                currentNumber++;

                CheckIfCorrect();
            }
        }
    }

    private void CheckIfCorrect()
    {
        if(!finishedPuzzle)
        {
            if (codeText[currentNumber] == correctCode[currentNumber])
            {
                Debug.Log("correct");

                if (codeText.Count == finalDigit)
                {
                    Debug.Log("You Got It");
                    StartCoroutine(CorrectCode());
                }
            }
            else
            {
                StartCoroutine(WrongCode());
            }
        }
    }
    IEnumerator WrongCode()
    {
        yield return new WaitForSeconds(0.1f);
        codeText.Clear();
        currentNumber = -1;
        Debug.Log("incorrect");
    }
    IEnumerator CorrectCode()
    {
        yield return new WaitForSeconds(1);
        finishedPuzzle = true;
        codeText.Clear();
        CloseTab();
        //DoActionFromCode
    }
    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
}
