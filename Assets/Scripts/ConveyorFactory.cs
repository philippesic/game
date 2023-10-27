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
        if (heldItem.displayObject == item.gameObject)
        {
            heldItem = null;
        }
    }

    public override bool HasRoomToPush()
    {
        return heldItem == null;
    }

    public override void Tick()
    {
        if (neighborFactories.ContainsKey("(0, 0, 1)"))
        {
            ItemObjectContainingFactory containingFactory = neighborFactories["(0, 0, 1)"].GetBlockFromType<ItemObjectContainingFactory>();
            if (heldItem == null)
            {
                shouldMoveItems = false;
            }
            else if (shouldMoveItems && containingFactory != null && containingFactory.HasRoomToPush())
            {
                shouldMoveItems = false;
                containingFactory.GiveItem(heldItem);
                RemoveItem(heldItem);
            }
        }
    }
}
