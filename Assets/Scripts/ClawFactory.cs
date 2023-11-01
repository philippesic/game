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

    public void TryPush()
    {
        if (shouldMoveItems)
        {
            if (neighborFactories.ContainsKey("(0, 0, 1)"))
            {
                ConveyorFactory factory = neighborFactories["(0, 0, 1)"].GetBlockFromType<ConveyorFactory>();
                if (factory != null && factory.HasRoomToPush())
                {
                    factory.GiveItem(RemoveItem(heldItem));
                    shouldMoveItems = false;
                }
                else
                {
                    WorldBlock block = neighborFactories["(0, 0, 1)"].GetBlockFromType<Factory>();
                    if (block != null)
                    {
                        shouldMoveItems = false;
                    }
                }
            }
            else
            {
                Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward, new Vector3(0.45f, 0.45f, 0.45f));
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out Item _))
                    {
                        shouldMoveItems = false;
                        break;
                    }
                }
                if (shouldMoveItems)
                {
                    RemoveItem(heldItem).displayObject.transform.position = transform.position + transform.forward;
                    shouldMoveItems = false;
                }
            }
        }
    }

    public void TryGrab()
    {
        if (shouldMoveItems)
        {
            if (neighborFactories.ContainsKey("(0, 0, -1)"))
            {
                ConveyorFactory conveyorFactory = neighborFactories["(0, 0, -1)"].GetBlockFromType<ConveyorFactory>();
                if (conveyorFactory != null && conveyorFactory.heldItem != null)
                {
                    GiveItem(conveyorFactory.RemoveItem(conveyorFactory.heldItem));
                    shouldMoveItems = false;
                }
                else
                {
                    Drill drill = neighborFactories["(0, 0, -1)"].GetBlockFromType<Drill>();
                    if (drill != null && drill.outputItems.Count() > 0)
                    {
                        GiveItem(GetItemGameObjectContainer(drill.outputItems.Get(1)));
                        shouldMoveItems = false;
                    }
                    else
                    {
                        Factory factory = neighborFactories["(0, 0, -1)"].GetBlockFromType<Factory>();
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
                            GiveItem(GetItemGameObjectContainer(collider.gameObject));
                            shouldMoveItems = false;
                            break;
                        }
                    }
                }

            }
        }
    }
}
