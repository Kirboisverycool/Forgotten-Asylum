using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightFlicker : MonoBehaviour
{
    public Light2D myLight;
    public float maxInterval = 1f;
    public float minWait;
    public float maxWait;
    float defautltIntensity;

   
    float interval;
    float timer;

  

    bool isOn;
    bool canFlicker;

    private void Start()
    {
        defautltIntensity = myLight.intensity;
        StartCoroutine(WaitToggle());
    }

    private void flicker()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            if (myLight.intensity == 0)
            {
                myLight.intensity = defautltIntensity;
            }
            else
            {
                myLight.intensity = defautltIntensity;
            }
            timer = 0;
            interval = Random.Range(0, maxInterval);


        }


    }
    void Update()
    {
        if (canFlicker)
        { 
            flicker();
        }
             
    }
    IEnumerator WaitToggle()
    {
        if (myLight.intensity <= 0)
        {
            myLight.intensity = defautltIntensity;
        }
        yield return new WaitForSeconds(Random.Range(minWait,maxWait));
        canFlicker = !canFlicker;
        StartCoroutine(WaitToggle());
    }
}
