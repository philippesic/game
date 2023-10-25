using UnityEngine;

public class PlayerInventory : UI
{
    public Transform ItemGrid;
    ItemContainer lastInv;

    public override void UIAwake() { lastInv = ScriptableObject.CreateInstance<ItemContainer>(); }

    void Update()
    {
        if (!(Player.instance.inv.Has(lastInv) && lastInv.Has(Player.instance.inv)))
        {
            SetGridItems(Player.instance.inv.inventoryItems, ItemGrid);
        }
        lastInv = ScriptableObject.CreateInstance<ItemContainer>(); ;
        lastInv.Add(Player.instance.inv);
    }
}