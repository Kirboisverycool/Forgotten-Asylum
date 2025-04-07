using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBack : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("TriggeredFlashBack");
        FindObjectOfType<FlashBackManager>().SetPassed();
        gameObject.SetActive(false);
    }
}
