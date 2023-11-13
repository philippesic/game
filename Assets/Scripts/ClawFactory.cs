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

    public override void Tick()
    {
        if (heldItem == null)
        {
            TryGrab();
        }
        else
        {
            TryPush();
        }
    }

    protected override void MannageGivenItem(ItemGameObjectContainer item)
    {
        heldItem = item;
    }

    public override void UpdateItemMovement()
    {
        heldItem?.item.UpdateMovement();
    }

    public override Vector3 GetInputPos()
    {
        return transform.position + transform.rotation * new Vector3(0, 0, -1);
    }

    public override Vector3 GetOutputPos()
    {
        return transform.position + transform.rotation * new Vector3(0, 0, 1);
    }

    public void TryPush()
    {
        if (heldItem.item.isMoving)
        {
            shouldMoveItems = false;
        }
        if (shouldMoveItems)
        {
            if (neighborFactories.ContainsKey("(0, 0, 1)"))
            {
                Factory neighborFactorie = neighborFactories["(0, 0, 1)"];
                ConveyorFactory conveyorFactory = neighborFactorie.GetBlockFromType<ConveyorFactory>();
                if (conveyorFactory != null && conveyorFactory.HasRoomToPush())
                {
                    conveyorFactory.Give(RemoveItem(heldItem));
                    shouldMoveItems = false;
                }
                else
                {
                    InventoryContainingFactory inventoryFactory = neighborFactorie.GetBlockFromType<InventoryContainingFactory>();
                    if (inventoryFactory != null && inventoryFactory.HasRoomToPush())
                    {
                        inventoryFactory.Give(RemoveItem(heldItem).item.Pop());
                        shouldMoveItems = false;
                    }
                    else
                    {
                        Factory block = neighborFactorie.GetBlockFromType<Factory>();
                        if (block != null)
                        {
                            shouldMoveItems = false;
                        }
                    }
                }
            }
            // makes claws place items on the ground
            // else
            // {
            //     Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward, new Vector3(0.45f, 0.45f, 0.45f));
            //     foreach (Collider collider in colliders)
            //     {
            //         if (collider.TryGetComponent(out Item _))
            //         {
            //             shouldMoveItems = false;
            //             break;
            //         }
            //     }
            //     if (shouldMoveItems)
            //     {
            //         RemoveItem(heldItem).displayObject.transform.position = transform.position + transform.forward;
            //         shouldMoveItems = false;
            //     }
            // }
        }
    }

    public void TryGrab()
    {
        if (shouldMoveItems)
        {
            if (neighborFactories.ContainsKey("(0, 0, -1)"))
            {
                Factory neighborFactorie = neighborFactories["(0, 0, -1)"];
                ConveyorFactory conveyorFactory = neighborFactorie.GetBlockFromType<ConveyorFactory>();
                if (conveyorFactory != null && conveyorFactory.heldItem != null && !conveyorFactory.heldItem.item.isMoving)
                {
                    Give(conveyorFactory.RemoveItem(conveyorFactory.heldItem));
                    shouldMoveItems = false;
                }
                else
                {
                    InventoryContainingFactory inventoryFactory = neighborFactorie.GetBlockFromType<InventoryContainingFactory>();
                    if (inventoryFactory != null)
                    {
                        Give(GetItemGameObjectContainer(inventoryFactory.Get(1)));
                        shouldMoveItems = false;
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
                Collider[] colliders = Physics.OverlapBox(transform.position - transform.forward, new Vector3(0.45f, 0.45f, 0.45f));
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out Item item))
                    {
                        if (item.containingFactory == null)
                        {
                            Give(GetItemGameObjectContainer(collider.gameObject));
                            shouldMoveItems = false;
                            break;
                        }
                    }
                }

            }
        }
    }
}
