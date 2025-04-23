using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    [SerializeField] float pitch;
    [SerializeField] float semiTones;

    public void PlayKey()
    {
        pitch = Mathf.Pow(2f, semiTones / 12);
        audioSource.pitch = 1 * pitch;
        audioSource.PlayOneShot(clip);
    }

    public void OnMouseOver()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            PlayKey();
        }
    }
}
