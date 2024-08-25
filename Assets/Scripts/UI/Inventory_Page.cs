using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class Inventory_Page : MonoBehaviour
    {
        [SerializeField]
        private Inventory_Item itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private RectTransform modifiersContentPanel;

        [SerializeField]
        private GameObject modifiersPanel;

        [SerializeField]
        private Inventory_Description itemDescription;

        [SerializeField]
        private GameObject itemDescriptionPanel;

        private bool itemDescriptionPanelOpen = false;

        [SerializeField]
        private MouseFollower mouseFollower;

        List<Inventory_Item> listOfItems = new List<Inventory_Item>();

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        [SerializeField]
        private ItemActionPanel actionPanel;

        private void Awake()
        {
            HideItem();
            mouseFollower.Toggle(false);
            HideDescription();
    }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                Inventory_Item uiItem =
                    Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);

                listOfItems.Add(uiItem);

                // Mouse Events
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeingDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfItems.Count > itemIndex)
            {
                listOfItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        // Mouse Event Methods
        private void HandleShowItemActions(Inventory_Item inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);

            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(Inventory_Item inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(Inventory_Item inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);

            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);

        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(Inventory_Item inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);

            if (index == -1) return;

            currentlyDraggedItemIndex = index;

            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);

        }

        public void CreateDraggedItem(Sprite i_sprite, int i_quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(i_sprite, i_quantity);
        }

        private void HandleItemSelection(Inventory_Item inventoryItemUI)
        {
            int index = listOfItems.IndexOf(inventoryItemUI);

            if (index == -1) return;

            OnDescriptionRequested?.Invoke(index);
        }

        // UI Methods

        public void ShowItem()
        {
            gameObject.SetActive(true);
            itemDescription.ResetDescription();
            ResetSelection();

        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            actionPanel.Toggle(false);

            modifiersPanel.SetActive(false);

        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfItems[itemIndex].transform.position;
        }

        public void HideItem()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void UpdateDescription(int itemIndex, Sprite descriptionImage, string itemName, string description, InventoryItem inventoryItem)
        {
            ResetSelection();
            itemDescription.SetDescription(descriptionImage, itemName, description);
            
            if(inventoryItem.itemState.Count == 0)
            {
                modifiersPanel.SetActive(false);
            } else
            {
                modifiersPanel.SetActive(true);
                itemDescription.SetModifiers(inventoryItem);
            }
            

        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfItems)
            {
                item.ResetData();
            }
        }

        private void HideDescription()
        {
            itemDescriptionPanel.SetActive(false);
            itemDescriptionPanelOpen = false;
            ResetSelection();
        }

        private void ShowDescription()
        {
            ResetSelection();
            itemDescriptionPanel.SetActive(true);
            itemDescriptionPanelOpen = true;
        }

        public void ToggleDescription()
        {
            if (itemDescriptionPanelOpen)
            {
                HideDescription();
            } else
            {
                ShowDescription();
            }
        }
    }
}