using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMirrors : MonoBehaviour
{
    [SerializeField] List<Sprite> mirrorSprites;
    [SerializeField] List<int> lightsRotation;
 
    [SerializeField] int currentIndex = 1;
    [SerializeField] SpriteRenderer mirrorRenderer;
    [SerializeField]public GameObject lightObj;
    [SerializeField] int correctePosition;
    [SerializeField]public bool isCorrect;
    private void Start()
    {
        mirrorRenderer.sprite = mirrorSprites[currentIndex];

        lightObj.transform.eulerAngles = new Vector3(0,0, lightsRotation[currentIndex]);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
       
        Debug.Log("MirrorRotate");
        if (currentIndex + 2 > mirrorSprites.Count)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        lightObj.transform.eulerAngles = new Vector3(0, 0, lightsRotation[currentIndex]);
        mirrorRenderer.sprite = mirrorSprites[currentIndex];
        if (currentIndex == correctePosition)
        {
            isCorrect = true;
        }
        else
        { 
            isCorrect = false;
        }
        gameObject.SetActive(false);
    }


}
