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
        Debug.Log("CanPush");
        if (containingFactory != null)
        {
            return containingFactory.HasRoomToPush();
        }
        return false;
    }

    public override bool HasRoomToPush()
    {
        Debug.Log("HasRoomToPush");
        return heldItem == null;
    }

    public override void Tick()
    {
        Debug.Log("Tick");
        if (neighborFactories.ContainsKey("(0, 0, 1)"))
        {
            Debug.Log(heldItem);
            ItemObjectContainingFactory containingFactory = neighborFactories["(0, 0, 1)"].GetBlockFromType<ItemObjectContainingFactory>();
            if (heldItem == null)
            {
                shouldMoveItems = false;
            }
            else if (shouldMoveItems && CanPush(containingFactory))
            {
                Debug.Log("Tick3");
                shouldMoveItems = false;
                containingFactory.GiveItem(heldItem);
                RemoveItem(heldItem);
            }
        }
    }
}
