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

    [SerializeField] public GameObject presentTimeScene;
    [SerializeField] public GameObject pastTimeScene;

    [Header("ReturnTimer")]
    [SerializeField] public int timeInPast;
    [SerializeField] GameObject returnTimerParent;
    [SerializeField] Image circleFill;
    [SerializeField] TextMeshProUGUI TimeleftText;
    
   
    // NEW SYSTEM
    // Cache the two past and present scenes
    // activate / deactivate as normal
    //on a door  open recache the scenes
    // Then trigger them to make sure the right version is active
    // function for re assgining the scenes, then at the end check id in past, and then if yes then run a new custom past sequence only switching the scene
    public bool isInPast;
    float timeSinceArrived;

    public void EnsureCorrectScene()
    {
        if (isInPast)
        {
            presentTimeScene.SetActive(false);
            pastTimeScene.SetActive(true);
        }
        else
        {
            presentTimeScene.SetActive(true);
            pastTimeScene.SetActive(false);
        }
    }
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
            pastTimeScene.SetActive(true);
            presentTimeScene.SetActive(false);
            SetPastTimer();
            StartCoroutine(ResetAnimation());

        }

        public void SetCurrent()
        {
            isInPast = false;
            returnTimerParent.SetActive(false);
            //  fadeParent.GetComponent<Animator>().SetTrigger("FlashFade");
            AudioSource.PlayClipAtPoint(returnSound, Camera.main.transform.position);
            presentTimeScene.SetActive(true);
            pastTimeScene.SetActive(false);
            StartCoroutine(ResetAnimation());

        }

        private IEnumerator ResetAnimation()
        {
            yield return new WaitForSeconds(2);
            // fadeParent.GetComponent<Animator>().ResetTrigger("FlashFade");
        }
        public void SwitchTime()
        {
            isInPastTime = !isInPastTime;

            if (isInPastTime)
            {
                presentTimeScene.SetActive(false);
                pastTimeScene.SetActive(true);
            }
            else
            {
                presentTimeScene.SetActive(true);
                pastTimeScene.SetActive(false);
            }
        }
    }
