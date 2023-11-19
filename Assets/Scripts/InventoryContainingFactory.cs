using UnityEngine;

public class InventoryContainingFactory : Factory
{
    public class ItemData : ItemContainer.ItemData
    {
        public ItemData(int id, int count) : base(id, count) { }
    }

    public virtual bool HasRoomToPush(Item item)
    {
        return HasRoomToPush(item.id, item.count);
    }
     public virtual bool HasRoomToPush(int id, int count)
    {
        return HasRoomToPush(ItemContainer.New().Add(id, count));
    }
     public virtual bool HasRoomToPush(ItemData item)
    {
        return HasRoomToPush(ItemContainer.New().Add(item.id, item.count));
    }
     public virtual bool HasRoomToPush(ItemContainer items)
    {
        return items.Count() == 0;
    }

    public ItemContainer Give(Item item)
    {
        return Give(item.id, item.count);
    }

    public ItemContainer Give(int id, int count)
    {
        return Give(ItemContainer.New().Add(id, count));
    }

    public ItemContainer Give(ItemData item)
    {
        return Give(ItemContainer.New().Add(item.id, item.count));
    }

    public virtual ItemContainer Give(ItemContainer items) { return ItemContainer.New(); }

    public virtual float GetProssesing0To1() {return 0;}

    public virtual ItemData Get(int count)
    {
        return null;
    }
}
