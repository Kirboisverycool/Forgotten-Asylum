using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] List<string> itemNames;
    [SerializeField] List<GameObject> piece;

    private void OnEnable()
    {
        string itemInHand = FindObjectOfType<InventoryScript>().HasItemInHand();
        for (int i = 0; i < itemNames.Count; i++)
        {
            if (itemInHand == itemNames[i])
            {
                piece[i].GetComponent<SpriteRenderer>().color = Color.white;
                Debug.Log(itemInHand);
                FindObjectOfType<InventoryScript>().RemoveFromInventory();

            }
        }
        gameObject.SetActive(false);

        //Debug.LogWarning("Broken");
        gameObject.SetActive(false);
    }
}
