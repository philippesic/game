using UnityEngine;

public class InventoryContainingFactory : Factory
{
    public class ItemData : ItemContainer.ItemData
    {
        public ItemData(int id, int count) : base(id, count) { }
    }

    public virtual bool HasRoomToPush(int count = 1)
    {
        return count == 0;
    }

    public void Give(Item item)
    {
        Give(item.id, item.count);
    }

    public void Give(int id, int count)
    {
        Give(new ItemData(id, count));
    }

    public virtual float GetProssesing0To1() {return 0;}

    public virtual void Give(ItemData item) { }

    public virtual ItemData Get(int count)
    {
        return null;
    }
}
