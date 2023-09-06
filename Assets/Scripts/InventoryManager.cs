using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : ItemContainer
{
    public Transform ItemContent;
    public GameObject InventoryItem;
    public KeyCode inventoryKey;
    public GameObject inventory;

    public bool isOpen = false;
    public static InventoryManager Instance;

    public ShopController shop;
    public GameObject placer;

    private void Awake()
    {
        Instance = this;
        inventory.SetActive(false);
    }

    public new void UpdateAfterContentChange() {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Item item in inventoryItems)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            ItemName.text = item.itemName + " " + "x" + item.count.ToString();;
            ItemIcon.sprite = item.icon;
        }
    }

    private void ToggleInventory()
    {
        if (inventory.activeSelf)
        {
            Time.timeScale = 1;
            inventory.SetActive(false);
            placer.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpen = false;
        }
        else
        {
            UpdateInventory();
            Time.timeScale = 0;
            inventory.SetActive(true);
            placer.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey) && ! false)
        {
            print(isOpen);
            ToggleInventory();
        }
    }
}