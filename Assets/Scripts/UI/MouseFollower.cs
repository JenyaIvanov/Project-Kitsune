using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{

    [SerializeField]
    private Canvas UICanvas;

    [SerializeField]
    private Inventory_Item item;

    private void Awake()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UICanvas").transform.GetComponent<Canvas>();
        item = GetComponentInChildren<Inventory_Item>();
    }

    public void SetData(Sprite i_sprite, int i_quantity)
    {
        item.SetData(i_sprite, i_quantity);
    }

    private void Update()
    {
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)UICanvas.transform, Input.mousePosition, UICanvas.worldCamera, out position);

        transform.position = UICanvas.transform.TransformPoint(position);
    }

    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }

}
