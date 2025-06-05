using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPuzzle : MonoBehaviour
{
    [SerializeField] List<RotateMirrors> mirrors;

    public void CheckForCorrect()
    {
        for (int i = 0; i < mirrors.Count; i++)
        {
            if (mirrors[i].isInRotationRange)
            {
                if (i + 1 > mirrors.Count)
                {
                    Debug.Log("FinishedMirrorPuzzle");
                }
                else
                {
                    mirrors[i + 1].reflectionLight.SetActive(true);
                }
              
            }
            else
            {
                Debug.Log("Negative");
                for (int j = i; j < mirrors.Count; j++)
                {
                    if (j + 1 > mirrors.Count)
                    {
                        return;
                    }
                    else
                    {
                        Debug.Log("SetLightFalse" + j + "," + i + mirrors[j + 1].gameObject);
                        mirrors[j + 1].reflectionLight.SetActive(false);
                        
                    }
                   
                }
                return;
               
            }

        }
    }   
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

}
