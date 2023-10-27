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
        if (heldItem.displayObject == item)
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
            Debug.Log("TryGrab");
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
                Collider[] colliders = Physics.OverlapBox(transform.position + Quaternion.Inverse(transform.rotation) * Vector3.forward, new Vector3(0.9f, 0.9f, 0.9f));
                shouldMoveItems = false;
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out Item item))
                    {
                        shouldMoveItems = true;
                        break;
                    }
                }
                if (shouldMoveItems)
                {
                    heldItem.displayObject.transform.position = transform.position + Quaternion.Inverse(transform.rotation) * Vector3.forward;
                    RemoveItem(heldItem);
                    shouldMoveItems = false;
                }
            }
        }
    }

    public void TryGrab()
    {
        if (shouldMoveItems)
        {
            PrintDict(neighborFactories);
            if (neighborFactories.ContainsKey("(0, 0, -1)"))
            {
                ConveyorFactory factory = neighborFactories["(0, 0, -1)"].GetBlockFromType<ConveyorFactory>();
                if (factory != null && factory.heldItem != null)
                {
                    GiveItem(factory.RemoveItem(factory.heldItem));
                    shouldMoveItems = false;
                }
                else
                {
                    WorldBlock block = neighborFactories["(0, 0, -1)"].GetBlockFromType<Factory>();
                    if (block != null)
                    {
                        shouldMoveItems = false;
                    }
                }
            }
            else
            {
                Collider[] colliders = Physics.OverlapBox(transform.position + Quaternion.Inverse(transform.rotation) * Vector3.forward, new Vector3(0.9f, 0.9f, 0.9f));
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out Item item))
                    {
                        if (item.containingFactory == null)
                        {
                            collider.gameObject.transform.position = transform.position + Quaternion.Inverse(transform.rotation) * Vector3.forward;
                            GiveItem(new ItemGameObjectContainer(collider.gameObject));
                            shouldMoveItems = false;
                            break;
                        }
                        // idfk
                    }
                }

            }
        }
    }
}
