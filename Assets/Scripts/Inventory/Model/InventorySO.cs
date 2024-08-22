using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO i_item, int i_quantity, List<ItemParameter> itemState = null)
        {
            if(i_item.IsStackable == false) { 
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(i_quantity > 0 && !IsInventoryFull())
                    {
                        i_quantity -= AddItemToFirstFreeSlot(i_item, 1, itemState);

                    }
                    InformAboutChange();
                    return i_quantity;

                }
            }

            i_quantity = AddStackableItem(i_item, i_quantity);
            InformAboutChange();

            return i_quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO i_item, int i_quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = i_item,
                quantity = i_quantity,
                itemState = new List<ItemParameter>(itemState == null ? i_item.DefaultParametersList : itemState),

            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {

                    inventoryItems[i] = newItem;
                    return i_quantity;
                   
                }
            }

            return 0;

        }

        private bool IsInventoryFull()
        => inventoryItems.Where(item => item.isEmpty).Any() == false;

        private int AddStackableItem(ItemSO i_item, int i_quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                    continue;
                if (inventoryItems[i].item.ID == i_item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStacckSize - inventoryItems[i].quantity;

                    if(i_quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStacckSize);
                        i_quantity -= amountPossibleToTake;
                    } else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + i_quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while(i_quantity > 0 && !IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(i_quantity, 0, i_item.MaxStacckSize);
                i_quantity -= newQuantity;
                AddItemToFirstFreeSlot(i_item, newQuantity);
            }
            return i_quantity;
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                    continue;

                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if(inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].isEmpty)
                    return;

                int remainder = inventoryItems[itemIndex].quantity - amount;

                if(remainder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(remainder);

                InformAboutChange();

            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool isEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
                itemState = new List<ItemParameter>()
            };
    }
}

