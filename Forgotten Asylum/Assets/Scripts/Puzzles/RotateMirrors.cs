using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMirrors : MonoBehaviour
{
    bool inRange;
    bool isRotating;
    [SerializeField] public GameObject reflectionLight;
    [SerializeField] GameObject controlText;
    [SerializeField] MirrorPuzzle puzzleParent;
    [SerializeField] float rotationRate;
    [SerializeField] float rotationDelay;
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    public bool isInRotationRange;
    public float zRotation;

    void Start()
    {
        controlText.SetActive(false);
        zRotation = gameObject.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKey(KeyCode.E))
        {
            if (gameObject.transform.eulerAngles.z > minRotation && gameObject.transform.eulerAngles.z < maxRotation)
            {

                Debug.Log("In Range");
                isInRotationRange = true;
            }
            else
            {
                isInRotationRange = false;
            }
          //  Debug.Log(gameObject.transform.eulerAngles.z);

            if (!isRotating)
            {
                isRotating = true;
                StartCoroutine(addRotation());

            }
            puzzleParent.CheckForCorrect();
       
        }


    }
    IEnumerator addRotation()
    {
        
        zRotation += rotationRate;
        gameObject.transform.eulerAngles = new Vector3(0, 0, zRotation);
        yield return new WaitForSeconds(rotationDelay);
        isRotating=false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            controlText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            controlText.SetActive(false);
          
        }
    }
}
