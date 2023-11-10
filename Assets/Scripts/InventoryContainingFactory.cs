using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainingFactory : Factory
{
    public class ItemData : ItemContainer.ItemData
    {
        public ItemData(int id, int count) : base(id, count) {}
    }

    public virtual bool HasRoomToPush()
    {
        return false;
    }

    public virtual void Give(ItemData item) {}

    public virtual ItemData Get(int count) {
        return null;
    }
}
