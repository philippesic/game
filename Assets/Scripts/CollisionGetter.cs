using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGetter : MonoBehaviour
{
    List<Collider> currentCollisions = new List<Collider>();
    void OnTriggerEnter(Collider other)
    {
        if (!currentCollisions.Contains(other))
        {
            currentCollisions.Add(other);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!currentCollisions.Contains(other))
        {
            currentCollisions.Add(other);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (currentCollisions.Contains(other))
        {
            currentCollisions.Remove(other);
        }
    }

    public List<Collider> getCurrentCollisions()
    {
        return currentCollisions;
    }
}
