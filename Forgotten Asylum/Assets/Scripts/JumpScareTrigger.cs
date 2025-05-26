using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareTrigger : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    bool hashit = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hashit)
        {
            
            AudioSource.PlayClipAtPoint(clip, transform.position, 2);
            hashit = true;
        }
    }
}
