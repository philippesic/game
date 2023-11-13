using UnityEngine;

public class ConveyorFactory : ItemObjectContainingFactory
{
    public ItemGameObjectContainer heldItem;

    protected override void MannageGivenItem(ItemGameObjectContainer item)
    {
        heldItem = item;
    }

    protected override void RemoveItemInternal(Item item)
    {
        if (heldItem != null && heldItem.displayObject == item.gameObject)
        {
            heldItem = null;
        }
    }

    public override void UpdateItemMovement()
    {
        heldItem?.item.UpdateMovement();
    }

    public override ItemContainer GetExtraBlockCost() // destroys held items
    {
        if (heldItem == null) { return null; }
        return ItemContainer.New().Add(heldItem.item.Pop());
    }

    public override bool HasRoomToPush()
    {
        return heldItem == null;
    }

    public override void Tick()
    {
        if (heldItem == null) {
            shouldMoveItems = false;
        }
        else if (neighborFactories.ContainsKey("(0, 0, 1)"))
        {
            ItemObjectContainingFactory containingFactory = neighborFactories["(0, 0, 1)"].GetBlockFromType<ItemObjectContainingFactory>();
            if (shouldMoveItems && containingFactory != null && containingFactory.HasRoomToPush())
            {
                shouldMoveItems = false;
                containingFactory.Give(heldItem);
                RemoveItem(heldItem);
            }
        }
    }
}
