using UnityEngine;

public class ConveyorFactory : ItemObjectContainingFactory
{
    public ItemGameObjectContainer heldItem;

    protected override void MannageGivenItem(ItemGameObjectContainer item)
    {
        heldItem = item;
    }

    public override void RemoveItem(Item item)
    {
        if (heldItem.displayObject == item.gameObject)
        {
            heldItem = null;
        }
    }

    public override bool CanPush(ItemObjectContainingFactory containingFactory)
    {
        if (containingFactory != null)
        {
            return containingFactory.HasRoomToPush();
        }
        return false;
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
            else if (shouldMoveItems && CanPush(containingFactory))
            {
                shouldMoveItems = false;
                containingFactory.GiveItem(heldItem);
                RemoveItem(heldItem);
            }
        }
    }
}
