using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentHeadgear : MonoBehaviour
{
    [SerializeField]
    private EquipableItemSO headgear;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private GameObject headgearObject;

    private SpriteRenderer headgearRenderer;
    private Animator headgearAniamtor;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    [SerializeField]
    private float directionAnimationCooldownTime = 300;

    private float directionAnimationTimer = 0;
    private string headgearSpriteDirection;

    private void Awake()
    {
        headgearRenderer = headgearObject.GetComponent<SpriteRenderer>();
        headgearAniamtor = headgearObject.GetComponent<Animator>();
        headgearSpriteDirection = "Front";
        directionAnimationTimer = 0;
    }



    public void Sprite_Direction(string direction)
    {
        if (headgear == null) return;

        Change_Sprite_Direction(direction);
        directionAnimationTimer = directionAnimationCooldownTime;
        headgearSpriteDirection = direction;
        
    }

    private void FixedUpdate()
    {
        if(directionAnimationTimer >= 0 && headgear != null)
        {
            directionAnimationTimer--;
        }

        if(directionAnimationTimer <= 0 && headgear != null)
        {
            Change_Sprite_Direction(headgearSpriteDirection);
        }
    }

    private void Change_Sprite_Direction(string direction)
    {
        if (directionAnimationTimer > 0) return;
        if (direction == "Front")
        {
            headgearRenderer.sprite = headgear.frontSprite;
            headgearAniamtor.SetBool("Side_View", false);
        }

        if (direction == "Side")
        {
            headgearRenderer.sprite = headgear.sideSprite;
            headgearAniamtor.SetBool("Side_View", true);
        }
    }

    public void SetHeadgear(EquipableItemSO headgearItemSO, List<ItemParameter> itemState)
    {
        if (headgear != null)
        {
            inventoryData.AddItem(headgear, 1, itemCurrentState);
        }

        directionAnimationTimer = 0;
        headgearAniamtor.runtimeAnimatorController = headgearItemSO.headgearAnimator;
        Sprite_Direction(playerController.playerDirection);

        this.headgear = headgearItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].valueToChange + parameter.valueToChange;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    valueToChange = newValue,
                };
            }
        }
    }
}
