using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSoundArea : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] bool hasEchoEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMouvement>().GroundSoundChange(audioClip);
        }
    }
}
