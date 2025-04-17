using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBackManager : MonoBehaviour
{
    bool isInPastTime = false;
    [SerializeField] GameObject PresentTime;
    [SerializeField] GameObject PastTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPassed()
    {
        PastTime.SetActive(true);
        PresentTime.SetActive(false);
   
    }
    public void SetCurrent()
    {
        PresentTime.SetActive(true);
        PastTime.SetActive(false);
    }
    public void SwitchTime()
    {
        isInPastTime = !isInPastTime;

        if (isInPastTime)
        {
            PresentTime.SetActive(false);
            PastTime.SetActive(true);
        }
        else
        {
            PresentTime.SetActive(true);
            PastTime.SetActive(false);
        }
    }
}
