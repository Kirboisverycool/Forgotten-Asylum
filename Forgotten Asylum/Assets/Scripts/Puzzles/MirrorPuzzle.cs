using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MirrorPuzzle : MonoBehaviour
{
    List<RotateMirrors> mirrorsList;
    public void CheckList()
    {
        for (int i = 0; i < mirrorsList.Count; i++)
        {
            if (mirrorsList[i].isCorrect)
            mirrorsList[i + 1].lightObj.SetActive(true);
        }
    }

}
