using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Piano : MonoBehaviour
{
    [Header("Code will be around 7 digits")]
    [SerializeField][Range(0, 9999999)] int correctCode;
    [SerializeField] string codeText;
    [SerializeField] TextMeshProUGUI codeTextTMP;

    // Start is called before the first frame update
    void Start()
    {
        codeText = "";
        UpdateDisplay();
    }
    public void pressButton(int num)
    {
        if (codeText.Length < 4)
        {
            codeText += num.ToString();
            UpdateDisplay();
            if (codeText.Length == 4)
            {
                CheckIfCorrect(codeText);
            }
        }
    }
    private void UpdateDisplay()
    {
        codeTextTMP.text = codeText;
    }
    private void CheckIfCorrect(string codeTested)
    {
        if (codeTested == correctCode.ToString())
        {
            Debug.Log("CorrectCode");
            StartCoroutine(CorrectCode());
        }
        else
        {
            Debug.Log("Incorect");
            StartCoroutine(WrongCode());

        }
    }
    IEnumerator WrongCode()
    {
        yield return new WaitForSeconds(1);
        codeText = "";
        UpdateDisplay();
    }
    IEnumerator CorrectCode()
    {
        yield return new WaitForSeconds(1);
        CloseTab();
        //DoActionFromCode
    }
    public void CloseTab()
    {
        Destroy(gameObject);
    }
}
