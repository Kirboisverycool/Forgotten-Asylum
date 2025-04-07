using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] List<ObjectData> inventoryItemList;
    [SerializeField] GameObject inventoryGrid;

    int ammountOfSlots;
    void Start()
    {
        ammountOfSlots = inventoryGrid.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool AddToInventory(ObjectData data)
    {
        if (inventoryItemList.Count < inventoryGrid.transform.childCount)
        {
        
            inventoryItemList.Add(data);
            return true;

        }
        else
        {
            Debug.Log("InventoryFull");
            return false;
        }

    }
    private void UpdateSlot()
    { 
        
    }

}
