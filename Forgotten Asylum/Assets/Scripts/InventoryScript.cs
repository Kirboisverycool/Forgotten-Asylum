using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] List<ObjectData> inventoryItemList;
    [SerializeField] List<GameObject> inventoryUI;
    [SerializeField] GameObject inventoryGrid;

    public int selectedSlot = 0;

    int ammountOfSlots;
    void Start()
    {       
        ammountOfSlots = inventoryGrid.transform.childCount;
        for (int i = 0; i < ammountOfSlots; i++)
        {
            inventoryUI.Add(inventoryGrid.transform.GetChild(i).gameObject);
            inventoryGrid.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public string HasItemInHand()
    {
        return inventoryItemList[selectedSlot].objectName;          
    }
    public bool AddToInventory(ObjectData data)
    {
        if (inventoryItemList.Count < inventoryGrid.transform.childCount)
        {
        
            inventoryItemList.Add(data);
            int index = inventoryItemList.IndexOf(data);
            inventoryUI[index].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
            inventoryUI[index].transform.GetChild(0).GetComponent<Image>().sprite = data.hotbarImage;
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
