using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int id;
    public int count;

    public void Pickup()
    {
        Player.instance.inv.Add(id, count);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Pickup();
    }
}
