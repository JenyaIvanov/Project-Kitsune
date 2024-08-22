using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class Inventory_Description : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text itemTitle;

        [SerializeField]
        private TMP_Text itemDescription;

        [SerializeField]
        private TMP_Text modifierText;

        [SerializeField]
        private RectTransform modifiersPanel;

        public void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false);
            itemTitle.text = "";
            itemDescription.text = "";

            foreach(RectTransform child in modifiersPanel)
            {
                Destroy(child.gameObject);
            }
        }

        public void SetDescription(Sprite i_sprite, string i_itemName, string i_itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = i_sprite;
            itemTitle.text = i_itemName;
            itemDescription.text = i_itemDescription;
        }

        public void SetModifiers(InventoryItem inventoryItem)
        {
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                TMP_Text uiItem =
                    Instantiate(modifierText, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(modifiersPanel);

                uiItem.text = inventoryItem.itemState[i].itemParameter.ParameterName
                + ": " + inventoryItem.itemState[i].valueToChange;

                uiItem.color = inventoryItem.itemState[i].itemParameter.ParameterColor;
            }
        }

    }
}