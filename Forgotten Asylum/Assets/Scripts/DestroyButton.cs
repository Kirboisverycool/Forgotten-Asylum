using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void DestroyObj(GameObject obj)
    {
        FindObjectOfType<PostProccesingInteracting>().ToggleBlur(false);
        player.GetComponent<PlayerMouvement>().enabled = true;
        Destroy(obj);
    }
}
