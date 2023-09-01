using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemContent;
    public GameObject InventoryItem;
    public KeyCode inventoryKey;
    public GameObject inventory;

    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();


    private void Awake() {
        Instance = this;
        inventory.SetActive(false); 
    }

    public void Add(Item item) {
        Items.Add(item);
    }

    public void Remove(Item item) {
        Items.Remove(item);
    }

    public void ListItems() {

        foreach (Transform item in ItemContent) {
           Destroy(item.gameObject); 
        }

        foreach (var item in Items) {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            ItemName.text = item.itemName;
            ItemIcon.sprite = item.icon; 
        }
    }

    private void ToggleInventory() {
        if (inventory.activeSelf)
        {
            Time.timeScale = 1;
            inventory.SetActive(false); 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            ListItems();
            Time.timeScale = 0;
            inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update() {
        if (Input.GetKeyDown(inventoryKey)) {
            ToggleInventory();
        }
    }
}
