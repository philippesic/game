using UnityEngine;

public class PlayerInventory : UIToggle
{
    public Transform ItemGrid;
    ItemContainer lastInv;

    public override void UIAwake() { lastInv = ItemContainer.New(); }

    void Update()
    {
        if (!(Player.instance.inv.Has(lastInv) && lastInv.Has(Player.instance.inv)))
        {
            SetGridItems(Player.instance.inv.inventoryItems, ItemGrid);
        }
        lastInv = ItemContainer.New();
        lastInv.Add(Player.instance.inv);
    }
}