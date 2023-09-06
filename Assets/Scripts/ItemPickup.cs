using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public void Pickup(GameObject gameObject) {
        if (gameObject.name == "Player") {
            gameObject.GetComponent<Player>().inv.Add(item);
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other) {
            Pickup(other.gameObject);
    }
}
