using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPad : MonoBehaviour
{
    [Header("Code must be 4 digits")]
    [SerializeField][Range(0,9999)] int correctCode;
    [SerializeField] string codeText;
    [SerializeField] TextMeshProUGUI codeTextTMP;
    // Start is called before the first frame update
    void Start()
    {
        codeText = "";
    }
    public void pressButton(int num)
    {
        if (codeText.Length < 4)
        {
            codeText += num.ToString();
            UpdateDisplay();
            if (codeText.Length == 4)
            { 
                        
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
            
        }
    }
    public void CloseTab()
    { 
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
