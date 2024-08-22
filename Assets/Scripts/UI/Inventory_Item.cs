using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class Inventory_Item : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quanityText;

        public event Action<Inventory_Item> OnItemClicked, OnItemDroppedOn, OnItemBeingDrag, OnItemEndDrag, OnRightMouseBtnClick;

        private bool emptyItem = true;

        public void Awake()
        {
            ResetData();

        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            emptyItem = true;
        }

        public void SetData(Sprite i_sprite, int i_quantity)
        {
            if (i_sprite == null) return;
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = i_sprite;
            quanityText.text = i_quantity.ToString();
            emptyItem = false;

        }


        public void OnPointerClick(PointerEventData pointerData)
        {

            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (emptyItem) return;

            OnItemBeingDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}