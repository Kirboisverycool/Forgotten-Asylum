using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPanels : MonoBehaviour
{
    public bool isInRange;
    [SerializeField] GameObject text;
    [SerializeField] KeyCode keyboardKey;
    [SerializeField] GameObject doorObject;
    [SerializeField] string unlockedItemName;
    [SerializeField] AudioClip BreakSound;
    [SerializeField] Sprite brokenImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(keyboardKey))
        {
            if(FindObjectOfType<InventoryScript>().HasItemInHand() == unlockedItemName)
            {
                FindAnyObjectByType<InventoryScript>().RemoveFromInventory();
                AudioSource.PlayClipAtPoint(BreakSound, transform.position);
                doorObject.SetActive(true);
                GetComponent<SpriteRenderer>().sprite = brokenImage;
                Destroy(text);
                Destroy(this);


            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            text.SetActive(false);
        }

    }
}
