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
    [SerializeField] float correcteRotation;
    public float minRotation;
    public float maxRotation;
    [SerializeField] GameObject target;

    public bool isInRotationRange;
    public float zRotation;
    float startZ;

    void Start()
    {
        
        controlText.SetActive(false);
        zRotation = gameObject.transform.eulerAngles.z;
    }
    private void getAngles()
    {
        Vector3 direction = target.transform.position - transform.position;
        correcteRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        minRotation = correcteRotation -5;
        maxRotation = correcteRotation +5;
        Quaternion lookRot = Quaternion.LookRotation(direction);

    }

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
