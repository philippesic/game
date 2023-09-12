using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int itemId;
    public int multiplier;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drill"))
        {
            Drill script = other.gameObject.GetComponent<Drill>();
            script.speed /= multiplier;
            script.itemId = itemId;
        }
    }
}