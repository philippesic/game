using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public void Pickup() {
        Player.instance.inv.Add(item);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other) {
            Pickup();
    }
}
