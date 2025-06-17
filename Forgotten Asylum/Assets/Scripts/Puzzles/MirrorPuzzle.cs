using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MirrorPuzzle : MonoBehaviour
{
    [SerializeField] List<RotateMirrors> mirrorsList;
   
    public void CheckList()
    {
        for (int i = 0; i < mirrorsList.Count; i++)
        {
            if (mirrorsList[i].isCorrect)
            {
                if (i >= mirrorsList.Count)
                {
                    Debug.Log("ALL CORRECT");
                }
                else
                {
                    mirrorsList[i + 1].lightObj.SetActive(true);
                }
               
            }
            else
            {
                i++;
                for (int j = i; j < mirrorsList.Count; j++)
                {
                    mirrorsList[i].lightObj.SetActive(false);
                }
                return;

            }
          
        }
    }

}
