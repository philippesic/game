using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawFactory : ItemObjectContainingFactory
{
    public ItemGameObjectContainer heldItem;

    protected override void MannageGivenItem(ItemGameObjectContainer item)
    {
        shouldMoveItems = false;
        heldItem = item;
        if (heldItem.displayObject != null)
        {
            heldItem.displayObject.GetComponent<Item>().UpdateItemObjectContainingFactory(this);
            heldItem.displayObject.transform.position = transform.position;
        }
    }

    public override void RemoveItem(Item item)
    {
        if (heldItem.displayObject == item)
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
            ConveyorFactory conveyorFactory = neighborFactories["(0, 0, 1)"].GetBlockFromType<ConveyorFactory>();
            if (heldItem == null)
            {
                shouldMoveItems = false;
            }
            else if (shouldMoveItems && CanPush(conveyorFactory))
            {
                shouldMoveItems = false;
                conveyorFactory.GiveItem(heldItem);
                //RemoveItem();
            }
        }
    }
}
