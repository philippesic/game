using UnityEngine;

public class InventoryManager : UI
{
    public Transform ItemContent;

    void Update()
    {
        setGridItems(Player.instance.inv.inventoryItems, ItemContent);
    }
}