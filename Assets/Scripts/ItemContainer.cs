using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemContainer : MonoBehaviour
{
    public List<Item> inventoryItems = new List<Item>();

    public void Add(Item item) {
        foreach (var invItem in inventoryItems) {
            if (invItem.itemName == item.itemName) {
                invItem.count+= item.count;
                UpdateAfterContentChange();
                return;
            }
        }

        inventoryItems.Add(item);
        UpdateAfterContentChange();
    }

    public void Add(List<Item> items) {
        foreach (Item item in items) {
            Add(item);
        }
    }

    public void Remove(string itemName, int count) {
        foreach (var invItem in inventoryItems) {
            if (invItem.itemName == itemName) {
                invItem.count -= count;
                if (invItem.count < 1) {
                    inventoryItems.Remove(invItem);
                }

                UpdateAfterContentChange();
                return;
            }
        }
    }

    public void UpdateAfterContentChange() {}

    //public bool hasItems(List<Tuple<string, int>>) {}
}
