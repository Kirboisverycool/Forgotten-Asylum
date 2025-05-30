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
                mirrors[i + 1].reflectionLight.SetActive(true);
            }
            else
            { 
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
