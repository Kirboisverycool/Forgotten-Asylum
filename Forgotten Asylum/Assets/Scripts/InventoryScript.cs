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
    [SerializeField] GameObject selectorUi;

    [SerializeField] int ammountOfSlots;
    private void Awake()
    {
        ammountOfSlots = inventoryGrid.transform.childCount;
        for (int i = 0; i < ammountOfSlots; i++)
        {
            inventoryUI.Add(inventoryGrid.transform.GetChild(i).gameObject);
            inventoryGrid.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }
    }
    void Start()
    {
        //TWEAK THE AWAKE

        selectorUi.transform.position = inventoryUI[0].transform.position;
    }
    private void Update()
    {
        UpdateSelectedSlot();
    }
    private void UpdateSelectedSlot()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedSlot+2 > ammountOfSlots)
            {
                selectedSlot = 0;             
            }
            else
            {
                 selectedSlot++;
            }
        }
  

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedSlot - 1 < 0)
            {
                selectedSlot = ammountOfSlots - 1;
                
            }
            else
            {
                selectedSlot--;
            }

        }
 
        selectorUi.transform.position = inventoryUI[selectedSlot].transform.position;
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
  

}
