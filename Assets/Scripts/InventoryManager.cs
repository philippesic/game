using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemContent;
    public GameObject InventoryItem;
    private Player player;
    
    public InventoryManager()
    {
        player = Player.instance;
    }

    private void Awake()
    {
        UpdateInventory();
        //Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        //Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Destroy(this.gameObject);
    }

    public void UpdateInventory()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Item item in player.inv.inventoryItems)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            ItemName.text = item.itemName + " " + "x" + item.count.ToString(); ;
            ItemIcon.sprite = item.icon;
        }
    }

    void Update()
    {
        UpdateInventory();
        print(player.inv.inventoryItems);
    }
}