using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashBackManager : MonoBehaviour
{
    bool isInPastTime = false;
    [Header("Effects")]
    [SerializeField] AudioClip flashBackSound;
    [SerializeField] AudioClip returnSound;
    [SerializeField] GameObject fadeParent;
    [Header("Objects")]
    [SerializeField] GameObject PresentTime;
    [SerializeField] GameObject PastTime;
    [Header("ReturnTimer")]
    [SerializeField] int timeInPast;
    [SerializeField] GameObject returnTimerParent;
    [SerializeField] Image circleFill;
    [SerializeField] TextMeshProUGUI TimeleftText;
    //
    bool isInPast;
    float timeSinceArrived;

    // Start is called before the first frame update
    void Start()
    {
        returnTimerParent.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isInPast)
        {
            timeSinceArrived += Time.deltaTime;
            int timeLeft = timeInPast - Mathf.RoundToInt(timeSinceArrived);
            TimeleftText.text = timeLeft.ToString();
            float fillA = timeInPast - timeSinceArrived;
            circleFill.fillAmount = fillA / timeInPast;
            if (timeSinceArrived >= timeInPast)
            {
                SetCurrent();
            }
        }
    }
    private void SetPastTimer()
    {
        returnTimerParent.SetActive(true);
        timeSinceArrived = 0;
        TimeleftText.text = timeInPast.ToString();
        circleFill.fillAmount = 1;
        isInPast = true;
    }
    public void SetPassed()
    {
      //  fadeParent.GetComponent<Animator>().SetTrigger("FlashFade");
        AudioSource.PlayClipAtPoint(flashBackSound, Camera.main.transform.position);
        PastTime.SetActive(true);
        PresentTime.SetActive(false);
        SetPastTimer();
        StartCoroutine(ResetAnimation());
        
    }
    public void SetCurrent()
    {
        isInPast = false;
        returnTimerParent.SetActive(false);
      //  fadeParent.GetComponent<Animator>().SetTrigger("FlashFade");
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
