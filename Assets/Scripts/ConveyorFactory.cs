using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorFactory : Factory
{
    public class ConveyorFactoryItem
    {
        public int id;
        public GameObject displayObject;

        public ConveyorFactoryItem(int id)
        {
            this.id = id;
        }

        public ConveyorFactoryItem(GameObject displayObject)
        {
            id = displayObject.GetComponent<Item>().id;
            this.displayObject = displayObject;
        }

        public ConveyorFactoryItem(int id, GameObject displayObject)
        {
            this.id = id;
            this.displayObject = displayObject;
        }
    }

    [HideInInspector] public bool shouldMoveItems = true;
    public ConveyorFactoryItem heldItem;

    public void GiveItem(ConveyorFactoryItem item)
    {
        shouldMoveItems = false;
        heldItem = item;
        if (heldItem.displayObject != null)
        {
            heldItem.displayObject.GetComponent<Item>().UpdateConveyor(this);
            heldItem.displayObject.transform.position = transform.position;
        }
    }

    public void RemoveItemOnbelt()
    {
        heldItem = null;
    }

    public bool CanPush(ConveyorFactory conveyorFactory)
    {
        if (conveyorFactory != null)
        {
            return conveyorFactory.heldItem == null;
        }
        return false;
    }

    public override void PreTick()
    {
        shouldMoveItems = true;
    }

    public override void Tick()
    {
        if (neighborFactories.ContainsKey("(1, 0, 0)"))
        {
            ConveyorFactory conveyorFactory = neighborFactories["(1, 0, 0)"].GetBlockFromType<ConveyorFactory>();
            if (heldItem == null)
            {
                shouldMoveItems = false;
            }
            else if (shouldMoveItems && CanPush(conveyorFactory))
            {
                shouldMoveItems = false;
                conveyorFactory.GiveItem(heldItem);
                RemoveItemOnbelt();
            }
        }
    }

    public void printDict(Dictionary<string, Factory> dict)
    {
        foreach (KeyValuePair<string, Factory> pair in dict)
        {
            Debug.Log(pair.Key + pair.Value.ToString());
        }
    }
}
