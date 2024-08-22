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

        private bool test;

        [SerializeField]
        private TMP_Text itemTitle;

        [SerializeField]
        private TMP_Text itemDescription;

        public void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false);
            itemTitle.text = "";
            itemDescription.text = "";
        }

        public void SetDescription(Sprite i_sprite, string i_itemName, string i_itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = i_sprite;
            itemTitle.text = i_itemName;
            itemDescription.text = i_itemDescription;
        }

    }
}