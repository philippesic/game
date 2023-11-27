using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClawFactory : ItemObjectContainingFactory
{
    public ItemGameObjectContainer heldItem;

    protected override void RemoveItemInternal(Item item)
    {
        if (heldItem.displayObject == item.gameObject)
        {
            heldItem = null;
        }
    }

    public override bool HasRoomToPush()
    {
        return false;
    }

    public override ItemContainer GetExtraBlockCost() // destroys held items
    {
        if (heldItem == null) { return null; }
        return ItemContainer.New().Add(heldItem.item.Pop());
    }

    public override bool TryToMoveItem()
    {
        if (heldItem == null)
        {
            return TryGrab();
        }
        else
        {
            return TryPush();
        }

    }

    protected override void MannageGivenItem(ItemGameObjectContainer item)
    {
        heldItem = item;
    }

    public override void UpdateItemMovement()
    {
        if (heldItem == null) { return; }
        if (heldItem.item.IsDestroyed())
        {
            heldItem = null;
            return;
        }
        heldItem.item.UpdateMovement();
    }

    public override Vector3 GetInputPos()
    {
        return transform.position + transform.rotation * new Vector3(0, 0, -1);
    }

    public override Vector3 GetOutputPos()
    {
        return transform.position /*+ transform.rotation * new Vector3(0, 0, 1)*/;
    }

    public bool TryPush()
    {
        if (shouldMoveItems)
        {
            if (neighborFactories.ContainsKey("(0, 0, 1)"))
            {
                Factory neighborFactorie = neighborFactories["(0, 0, 1)"];
                if (neighborFactorie.GetBlockFromType(out ConveyorFactory conveyorFactory) && conveyorFactory.HasRoomToPush())
                {
                    conveyorFactory.Give(RemoveItem(heldItem));
                    shouldMoveItems = false;
                    return true;
                }
                else
                {
                    if (neighborFactorie.GetBlockFromType(out InventoryContainingFactory inventoryFactory) && inventoryFactory.HasRoomToPush(heldItem.item))
                    {
                        inventoryFactory.Give(RemoveItem(heldItem).item.Pop());
                        shouldMoveItems = false;
                        return true;
                    }
                }
            }
            else
            {
                shouldMoveItems = false;
            }
        }
        return false;
    }

    public bool TryGrab()
    {
        if (shouldMoveItems)
        {
            if (neighborFactories.ContainsKey("(0, 0, -1)"))
            {
                Factory neighborFactorie = neighborFactories["(0, 0, -1)"];
                ConveyorFactory conveyorFactory = neighborFactorie.GetBlockFromType<ConveyorFactory>();
                if (conveyorFactory != null && conveyorFactory.heldItem != null)
                {
                    Give(conveyorFactory.RemoveItem(conveyorFactory.heldItem));
                    shouldMoveItems = false;
                    return true;
                }
                else
                {
                    InventoryContainingFactory inventoryFactory = neighborFactorie.GetBlockFromType<InventoryContainingFactory>();
                    if (inventoryFactory != null)
                    {
                        Give(GetItemGameObjectContainer(inventoryFactory.Get(1)));
                        shouldMoveItems = false;
                        return true;
                    }
                    else
                    {
                        Factory factory = neighborFactorie.GetBlockFromType<Factory>();
                        if (factory != null)
                        {
                            shouldMoveItems = false;
                        }
                    }
                }
            }
            else
            {
                shouldMoveItems = false;
            }
            // else
            // {
            //     Collider[] colliders = Physics.OverlapBox(transform.position - transform.forward, new Vector3(0.45f, 0.45f, 0.45f));
            //     foreach (Collider collider in colliders)
            //     {
            //         if (collider.TryGetComponent(out Item item))
            //         {
            //             if (item.containingFactory == null)
            //             {
            //                 Give(GetItemGameObjectContainer(collider.gameObject));
            //                 shouldMoveItems = false;
            //                 break;
            //             }
            //         }
            //     }
            // }
        }
        return false;
    }
}
