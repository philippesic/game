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
    public List<ItemStack> ItemStacks = new List<ItemStack>();

    private void Awake()
    {
        Instance = this;
        inventory.SetActive(false);
    }

    public void Add(Item item)
    {
        
        foreach (var stack in ItemStacks)
        {
            if (stack.item == item)
            {
                stack.quantity++; 
                ListItems(); 
                return;
            }
        }

        
        ItemStack newItemStack = new ItemStack(item);
        ItemStacks.Add(newItemStack);
        ListItems(); 
    }

    public void Remove(ItemStack stack)
    {
        ItemStacks.Remove(stack);
        ListItems(); 
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var stack in ItemStacks)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            ItemName.text = stack.item.itemName + " " + "x" + stack.quantity.ToString();;
            ItemIcon.sprite = stack.item.icon;
        }
    }

    private void ToggleInventory()
    {
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

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
    }
}

[System.Serializable]
public class ItemStack
{
    public Item item;
    public int quantity;

    public ItemStack(Item item)
    {
        this.item = item;
        quantity = 1;
    }
}
