using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemContent;
    public GameObject InventoryItem;

    public void UpdateInventory()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Player.instance.inv.inventoryItems)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            ItemName.text = AllItemData.names[item.id] + " " + "x" + item.count.ToString();
            ItemIcon.sprite = AllItemData.icons[item.id];
        }
    }

    void Update()
    {
        UpdateInventory();
    }

    public void ToggleOff(GameObject inventory) {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventory.SetActive(false);
    }

        public void ToggleOn(GameObject inventory) {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inventory.SetActive(true);
    }
}