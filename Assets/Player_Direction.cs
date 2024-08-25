using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Direction : MonoBehaviour
{
    [SerializeField]
    private AgentHeadgear playerHeadgear;

    public void Facing_Front()
    {
        playerHeadgear.Sprite_Direction("Front");
    }


    public void Facing_Side()
    {
        playerHeadgear.Sprite_Direction("Side");
    }
}
