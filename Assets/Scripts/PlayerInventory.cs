using UnityEngine;

public class PlayerInventory : UI
{
    public Transform ItemGrid;

    void Update()
    {
        setGridItems(Player.instance.inv.inventoryItems, ItemGrid);
    }
}