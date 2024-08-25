using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquipableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip actionSFX {get; private set;}

        // Sprite & Animator
        [SerializeField]
        public Sprite frontSprite;

        [SerializeField]
        public Sprite sideSprite;

        [SerializeField]
        public AnimatorController headgearAnimator;


        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentHeadgear agentSystem = character.GetComponent<AgentHeadgear>();
            if(agentSystem != null)
            {
                agentSystem.SetHeadgear(this, itemState == null ? DefaultParametersList : itemState);
                return true;
            }
            return false;
        }
    }
}