using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClawFactory : ItemObjectContainingFactory
{
    public ItemGameObjectContainer heldItem;

    public override void RemoveItem(Item item)
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
            TryPush();
        }
        else
        {
            TryGrab();
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
                ItemObjectContainingFactory factory = neighborFactories["(0, 0, 1)"].GetBlockFromType<ItemObjectContainingFactory>();
                if (factory != null && factory.HasRoomToPush())
                {
                    shouldMoveItems = false;
                    factory.GiveItem(heldItem);
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
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out Item item))
                    {
                        shouldMoveItems = false;
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
            if (neighborFactories.ContainsKey("(0, 0, -1)"))
            {
                ItemObjectContainingFactory factory = neighborFactories["(0, 0, -1)"].GetBlockFromType<ItemObjectContainingFactory>();
                if (factory != null && factory.HasRoomToPush())
                {
                    shouldMoveItems = false;
                    factory.GiveItem(heldItem);
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
                        if (shouldMoveItems)
                        {
                            collider.gameObject.transform.position = transform.position + Quaternion.Inverse(transform.rotation) * Vector3.forward;
                            GiveItem(new ItemGameObjectContainer(collider.gameObject));
                            shouldMoveItems = false;
                        }
                    }
                }

            }
        }
    }
}
