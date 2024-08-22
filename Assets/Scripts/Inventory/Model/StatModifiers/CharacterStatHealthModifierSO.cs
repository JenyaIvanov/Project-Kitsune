using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float valueToModify)
    {
        // Get health from the character.
        // For reference watch Edible Item - Inventory System in Unity tutorial P19 (13:00)
        // https://www.youtube.com/watch?v=SjPDglLjTQc&list=PLcRSafycjWFegXSGBBf4fqIKWkHDw_G8D&index=19
    }
}
