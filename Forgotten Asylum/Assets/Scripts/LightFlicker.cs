using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightFlicker : MonoBehaviour
{
    public Light2D myLight;
    public float maxInterval = 1f;

    float targetIntensity;
    float lastIntensity;
    float interval;
    float timer;

    public float maxDisplacement = 0.25f;
  


    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            lastIntensity = myLight.intensity;
            targetIntensity = Random.Range(0.5f, 3f);
            timer = 0;
            interval = Random.Range(0, maxInterval);


        }

             myLight.intensity = Mathf.Lerp(lastIntensity, targetIntensity, timer / interval);
             
    }
}
