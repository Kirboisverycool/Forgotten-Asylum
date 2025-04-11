using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSoundArea : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMouvement>().GroundSoundChange(audioClip);
        }
    }
}
