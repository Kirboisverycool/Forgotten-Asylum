using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    SpriteMask mask;

    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<SpriteMask>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Feet")
        {
            mask.frontSortingOrder = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Feet")
        {
            mask.frontSortingOrder = -1;
        }
    }
}
