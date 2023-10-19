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

    public bool shouldMoveItems = true;
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
        ConveyorFactory conveyorFactory = neighborFactories["(0, 0, 1)"].gameObject.GetComponent<ConveyorFactory>();
        if (shouldMoveItems && heldItem != null && CanPush(conveyorFactory))
        {
            shouldMoveItems = false;
            conveyorFactory.GetItem(heldItem);
            heldItem = null;
        }
    }
}
