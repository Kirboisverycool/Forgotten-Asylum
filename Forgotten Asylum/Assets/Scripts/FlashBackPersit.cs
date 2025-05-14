using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBackPersit : MonoBehaviour
{
    private void Awake()
    {
        int numberOf = FindObjectsOfType<FlashBackPersit>().Length;
        if (numberOf > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);

        }
    }
    public void GetFlashBackData()
    {
        if (FindObjectOfType<FlashBackManager>() != null)
        {
            isInFlashBack = FindObjectOfType<FlashBackManager>().isInPast;
            timePast = FindObjectOfType<FlashBackManager>().timeInPast;
        }
      
      
        
    }

    public bool isInFlashBack;
    public float timePast;


    
    
}
