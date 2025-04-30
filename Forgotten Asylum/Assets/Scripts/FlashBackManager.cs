using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBackManager : MonoBehaviour
{
    bool isInPastTime = false;
    [SerializeField] AudioClip flashBackSound;
    [SerializeField] AudioClip returnSound;
    [SerializeField] GameObject fadeParent;
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
        fadeParent.GetComponent<Animator>().SetTrigger("FlashFade");
        AudioSource.PlayClipAtPoint(flashBackSound, Camera.main.transform.position);
        PastTime.SetActive(true);
        PresentTime.SetActive(false);
        StartCoroutine(ResetAnimation());
    }
    public void SetCurrent()
    {
        fadeParent.GetComponent<Animator>().SetTrigger("FlashFade");
        AudioSource.PlayClipAtPoint(returnSound, Camera.main.transform.position);
        PresentTime.SetActive(true);
        PastTime.SetActive(false);
        StartCoroutine(ResetAnimation());
    }
    private IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(2);
        fadeParent.GetComponent<Animator>().ResetTrigger("FlashFade");
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
