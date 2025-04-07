using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ObjectData
{
    [SerializeField] Sprite hotbarImage;
    [SerializeField] string objectName;
    [SerializeField] GameObject defaultObject;
}
public class ObjectPickupClass : MonoBehaviour
{


   [SerializeField] public ObjectData inventoryData = new ObjectData();
    private void OnEnable()
    {
        Debug.Log("Pickup");
        if (FindObjectOfType<InventoryScript>().AddToInventory(inventoryData))
        { 
            Destroy(gameObject.transform.parent.gameObject);
        }
        
    }
}
