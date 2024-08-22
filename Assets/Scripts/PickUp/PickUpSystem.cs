using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter(Collider collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int remainder = inventoryData.AddItem(item.inventoryItem, item.Quantity);
            if (remainder == 0)
                item.DestroyItem();
            else
                item.Quantity = remainder;
        }
    }

}
