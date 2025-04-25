using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{
   
    public void DestroyObj(GameObject obj)
    {
        FindObjectOfType<PostProccesingInteracting>().ToggleBlur(false);
        Destroy(obj);
    }
}
