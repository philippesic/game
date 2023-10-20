using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ConveyorFactory : Factory
{
    public class ConveyorFactoryItem
    {
        public int id;
        public GameObject itemDisplay;
        public ConveyorFactoryItem(int id)
        {
            this.id = id;
        }
    }
    [HideInInspector] public bool shouldMoveItems = true;
    public ConveyorFactoryItem heldItem;

    public void GetItem(ConveyorFactoryItem item)
    {
        shouldMoveItems = false;
        heldItem = item;
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
        if (neighborFactories.ContainsKey("(0, 0, 1)"))
        {
            ConveyorFactory conveyorFactory = neighborFactories["(0, 0, 1)"].gameObject.GetComponent<ConveyorFactory>();
            if (heldItem == null)
            {
                shouldMoveItems = false;
            }
            else if (shouldMoveItems && CanPush(conveyorFactory))
            {
                shouldMoveItems = false;
                conveyorFactory.GetItem(heldItem);
                heldItem = null;
            }
        }
    }
}
