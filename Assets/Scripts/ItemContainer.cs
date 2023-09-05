using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemContainer : MonoBehaviour
{
    public GameObject inventoryItem;
    public KeyCode inventoryKey;
    public GameObject inventory;
    public List<Item> inventoryItems = new List<Item>();

    private void Awake() {
    }

    public void Add(Item item) {
        
        foreach (var invItem in inventoryItems) {
            if (invItem.itemName == item.itemName) {
                invItem.count+= item.count; 
                return;
            }
        }

        inventoryItems.Add(item);
    }

    public void Add(List<Item> items) {
        foreach (Item item in items) {
            Add(item);
        }
    }

    public void RemoveItem(string itemName, int count) {
        foreach (var invItem in inventoryItems) {
            if (invItem.itemName == itemName) {
                invItem.count -= count; 
                return;
            }
        }
    }


}
