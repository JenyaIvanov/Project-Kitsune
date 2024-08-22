using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{

    private HashSet<Collider> colliders = new HashSet<Collider>();
    public HashSet<Collider> GetColliders() { return colliders; }
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.layer == LayerMask.NameToLayer("Enemies"))) { return; }
        print(other.name + " has entered colliders.");
        colliders.Add(other); 
    }

    private void OnTriggerExit(Collider other)
    {
        print(other.name + " has left colliders.");
        colliders.Remove(other);
    }

    public Collider GetClosestCollider(Transform playerPosition)
    {
        Collider closestCollider = null;
        float closestDistance = 99999.0f;

        foreach(Collider collider in colliders)
        {
            if(collider == null)
            {
                colliders.Remove(collider);

            } else {
                float distance = Vector3.Distance(playerPosition.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }

        }

        return closestCollider;
    }
}
