using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightFlicker : MonoBehaviour
{
    public Light2D myLight;
    public float maxInterval = 1f;
    public float minWaitNormal;
    public float maxWaitNormal;

    public float minWaitFlicker;
    public float maxWaitFlicker;

    float defautltIntensity;

   
    float interval;
    float timer;
    float waitTime;
  

  
    bool canFlicker;

    private void Start()
    {
        defautltIntensity = myLight.intensity;
        StartCoroutine(WaitToggle());
    }

    private void flicker()
    {



    }
    void Update()
    {
        if (canFlicker)
        {
            // flicker();
            timer += Time.deltaTime;
          
            if (timer > interval)
            {
                if (myLight.intensity == 0)
                {
                    myLight.intensity = defautltIntensity;
                }
                else
                {
                    myLight.intensity = 0;
                }
                timer = 0;
                interval = Random.Range(0, maxInterval);
            

            }
        }
             
    }
    IEnumerator WaitToggle()
    {
        if (canFlicker == false)
        {
            myLight.intensity = defautltIntensity;
        }
        if (!canFlicker)
        {
            waitTime = (Random.Range(minWaitNormal, maxWaitNormal));
        }
        else
        {
            waitTime = (Random.Range(minWaitFlicker, maxWaitFlicker));
        }
        yield return new WaitForSeconds(waitTime) ;
        canFlicker = !canFlicker;
        
        StartCoroutine(WaitToggle());
    }
}
