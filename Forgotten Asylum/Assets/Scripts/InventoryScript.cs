using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] List<ObjectData> inventoryItemList;
    [SerializeField] List<GameObject> inventoryUI;
    [SerializeField] GameObject inventoryGrid;
    [SerializeField] GameObject itemNameText;
    public int selectedSlot = 0;
    [SerializeField] GameObject selectorUi;

    [SerializeField] int ammountOfSlots;
    [SerializeField] KeyCode[] inputs;

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
        UpdateSlotWithNumbers();
    }

    private void UpdateSlotWithNumbers()
    {
        for(int input = 0; input < inputs.Length; input++)
        {
            if (Input.GetKeyDown(inputs[input]))
            {
                selectedSlot = input;
                break;
            }
        }
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
            NameTextUpdate();
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
            NameTextUpdate();
        }

        
        selectorUi.transform.position = inventoryUI[selectedSlot].transform.position;

    }
    public void NameTextUpdate()
    {
        if (selectedSlot+1 <= inventoryItemList.Count)
        {
            itemNameText.GetComponent<TextMeshProUGUI>().text = inventoryItemList[selectedSlot].objectName;
        }
        else
        {
            itemNameText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public string HasItemInHand()
    {
        if (inventoryItemList[selectedSlot] != null)
        {
            return inventoryItemList[selectedSlot].objectName;
        }
        else
        {
            return null;
        }
       
    }
    public void RemoveFromInventory()
    {
        inventoryItemList.RemoveAt(selectedSlot);
        inventoryUI[selectedSlot].transform.GetChild(0).GetComponent<Image>().sprite = null;
       inventoryUI[selectedSlot].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
        NameTextUpdate();
        ReAdjustInventory();
    }
    private void ReAdjustInventory()
    {
        int uiSlot = 0;
        foreach (var ObjData in inventoryItemList) 
        {
            inventoryUI[uiSlot].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
            inventoryUI[uiSlot].transform.GetChild(0).GetComponent<Image>().sprite = ObjData.hotbarImage;
            uiSlot++;
        }
        for(int i = 0; i < inventoryUI.Count; i ++)
        {
            inventoryUI[i].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
        }
    }
    public bool AddToInventory(ObjectData data)
    {
        if (inventoryItemList.Count < inventoryGrid.transform.childCount)
        {
        
            inventoryItemList.Add(data);
            int index = inventoryItemList.IndexOf(data);
            inventoryUI[index].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
            inventoryUI[index].transform.GetChild(0).GetComponent<Image>().sprite = data.hotbarImage;

            NameTextUpdate();
            return true;


        }
        else
        {
            Debug.Log("InventoryFull");
            return false;
        }

    }
  

}
