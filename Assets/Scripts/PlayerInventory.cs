using UnityEngine;

public class PlayerInventory : UI
{
    public Transform ItemGrid;

    void Update()
    {
        SetGridItems(Player.instance.inv.inventoryItems, ItemGrid);
    }
}