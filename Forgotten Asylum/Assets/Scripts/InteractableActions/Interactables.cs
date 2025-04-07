using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject nearText;
    [SerializeField] KeyCode interactKey;
    [SerializeField] GameObject objectToTrigger;

    public bool isNearest = false;
    CircleCollider2D rangeTrigger;


    private void Update() 
    {
        if (Input.GetKeyDown(interactKey) && isNearest)
        {
            Debug.Log("Interact");
            objectToTrigger.SetActive(true);
        }
    }
    void Start()
    {
        rangeTrigger = GetComponent<CircleCollider2D>();
        rangeTrigger.radius = range;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            FindObjectOfType<InteractManager>().AddToList(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<InteractManager>().RemoveFromList(gameObject);
            nearText.SetActive(false);
        }
    }
    public void IsNearest()
    {
        nearText.SetActive(true);
        isNearest = true;
    }
    public void NotNearest()
    {
        nearText.SetActive(false);
        isNearest = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range*transform.localScale.y );
    }
}
