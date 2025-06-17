using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MirrorPuzzle : MonoBehaviour
{
    [SerializeField] List<RotateMirrors> mirrorsList;
    [SerializeField] GameObject secretEntrance;
    public void CheckList()
    {
        if (mirrorsList.Count == 0) return;

        mirrorsList[0].lightObj.SetActive(true);

        bool allCorrect = mirrorsList[0].isCorrect; // Start with the first

        for (int i = 1; i < mirrorsList.Count; i++)
        {
            if (mirrorsList[i - 1].isCorrect)
            {
                mirrorsList[i].lightObj.SetActive(true);
                allCorrect = allCorrect && mirrorsList[i].isCorrect;
            }
            else
            {
                // Turn off this and everything after
                for (int j = i; j < mirrorsList.Count; j++)
                {
                    mirrorsList[j].lightObj.SetActive(false);
                }
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            OnPuzzleComplete();
        }
    }

    private void OnPuzzleComplete()
    {
        Debug.Log("Puzzle Completed! All mirrors are correct.");
        secretEntrance.SetActive(true); 
    }

}
