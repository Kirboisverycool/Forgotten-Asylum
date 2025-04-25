using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScrollingLock : MonoBehaviour
{
    List<int> correctSequence;
    List<int> sequence;
    List<TextMeshProUGUI> numText;
    void Start()
    {
        
    }
    public void UpdateNumber(int index)
    {
        sequence[index]++;
        updateNumText(index, sequence[index]);
    }
   //Check
    private void updateNumText(int index, int number)
    {
        numText[index].text = number.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
