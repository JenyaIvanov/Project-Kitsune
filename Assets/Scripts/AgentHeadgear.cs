using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHeadgear : MonoBehaviour
{
    [SerializeField]
    private EquipableItemSO headgear;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    public void SetHeadgear(EquipableItemSO headgearItemSO, List<ItemParameter> itemState)
    {
        if (headgear != null)
        {
            inventoryData.AddItem(headgear, 1, itemCurrentState);
        }

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
