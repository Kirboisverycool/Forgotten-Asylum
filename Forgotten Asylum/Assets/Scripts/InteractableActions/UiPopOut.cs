using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPopOut : MonoBehaviour
{
    [SerializeField] GameObject ui;
    private void OnEnable()
    {
        FindObjectOfType<PostProccesingInteracting>().ToggleBlur(true);
        var uiIrl = Instantiate(ui);
        uiIrl.transform.SetParent(GameObject.FindWithTag("MainCanvas").transform, false);
       

            gameObject.SetActive(false);
        
 
    }
}
