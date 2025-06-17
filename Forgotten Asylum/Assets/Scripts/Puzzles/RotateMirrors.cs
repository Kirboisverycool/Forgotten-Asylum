using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class RotateMirrors : MonoBehaviour
{
    [SerializeField] List<Sprite> mirrorSprites;
    [SerializeField] List<int> lightsRotation;
 
    [SerializeField] int currentIndex = 1;
    [SerializeField] SpriteRenderer mirrorRenderer;
    [SerializeField]public GameObject lightObj;
    [SerializeField] int correctePosition;
    [SerializeField]public bool isCorrect;

    [SerializeField] Color wrongColor;
    [SerializeField] Color correctColor;
    private void Start()
    {
        mirrorRenderer.sprite = mirrorSprites[currentIndex];

        lightObj.transform.eulerAngles = new Vector3(0,0, lightsRotation[currentIndex]);
        gameObject.SetActive(false);
        lightObj.GetComponent<Light2D>().color = wrongColor;
    }
    private void OnEnable()
    {
       
       
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

            FindObjectOfType<MirrorPuzzle>().CheckList();
            lightObj.GetComponent<Light2D>().color = correctColor;
        }
        else
        {
            isCorrect = false;
            FindObjectOfType<MirrorPuzzle>().CheckList();
            lightObj.GetComponent<Light2D>().color = wrongColor;
            
        }
        gameObject.SetActive(false);
    }
   
  


}
