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
    [SerializeField] float targetBuffer;
    [SerializeField] GameObject raycastAim;

    public bool isInRotationRange;
    public float zRotation;
    float startZ;

    void Start()
    {
        controlText.SetActive(false);
        zRotation = gameObject.transform.eulerAngles.z;

    }
    

    void Update()
    {
        if (inRange && Input.GetKey(KeyCode.E))
        {
           
            if (RayCheck())
            {
                puzzleParent.CheckForCorrect();
            }
          
          //  Debug.Log(gameObject.transform.eulerAngles.z);

            if (!isRotating)
            {
                isRotating = true;
                StartCoroutine(addRotation());

            }
          
       
        }


    }
    private bool RayCheck()
    {

        RaycastHit hit;
        Debug.DrawLine(transform.position, raycastAim.transform.position , Color.green);
      
        if (Physics.Raycast(transform.position, raycastAim.transform.position, out hit , 100))
        {
            Debug.Log("Hit");
            if (hit.Equals(target.transform.GetChild(1)))
            {
                isInRotationRange = true;
                return true;
              
            }
            isInRotationRange = false;
            return false;

        }
        else
        {
            isInRotationRange = false;
            return false;

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
