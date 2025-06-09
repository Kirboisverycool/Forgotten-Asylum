using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeNote : MonoBehaviour
{
    RandomNumberManager randomNumberManager;
    TextMeshProUGUI hintText;
    [SerializeField] string hint;

    // Start is called before the first frame update
    void Start()
    {
        randomNumberManager = FindObjectOfType<RandomNumberManager>();
        hintText = GetComponentInChildren<TextMeshProUGUI>();

        hintText.text = hint + " ";

        for(int i = 0; i < randomNumberManager.scrollingLockCode.Count; i++)
        {
            hintText.text += randomNumberManager.scrollingLockCode[i].ToString();
        }
    }
}
