using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;

public class ItemContainer : ScriptableObject
{
    public static ItemContainer New(int slotCount = -1)
    {
        ItemContainer itemContainer =  CreateInstance<ItemContainer>();
        itemContainer.slotCount = slotCount;
        return itemContainer;
    }

    public class ItemData
    {
        public int id;
        public int count;

        public ItemData(int id, int count)
        {
            this.id = id;
            this.count = count;
        }

        public ItemData() { }
    }

    public List<ItemData> inventoryItems = new();
    public int slotCount = -1;

    public ItemContainer Copy()
    {
        return New(slotCount).Add(this);
    }

    public int Count()
    {
        int count = 0;
        foreach (ItemData item in inventoryItems)
        {
            count += item.count;
        }
        return count;
    }

    public ItemData Get(int count)
    {
        if (inventoryItems.Count > 0)
        {
            if (inventoryItems[0].count > count)
            {
                inventoryItems[0].count -= count;
                ContentChange();
                return new ItemData(inventoryItems[0].id, count);
            }
            else
            {
                ItemData item = inventoryItems[0];
                inventoryItems.RemoveAt(0);
                ContentChange();
                return item;
            }
        }
        return null;
    }

    public List<int> GetIDs()
    {
        List<int> ids = new();
        foreach (ItemData item in inventoryItems)
        {
            ids.Add(item.id);
        }
        return ids;
    }

    public List<int> GetCounts()
    {
        List<int> counts = new();
        foreach (ItemData item in inventoryItems)
        {
            counts.Add(item.count);
        }
        return counts;
    }

    // methonds to add items
    public ItemContainer Add(Item item)
    {
        return Add(item, out _);
    }
    
    public ItemContainer Add(Item item, out ItemContainer leftovers)
    {
        return Add(item.id, item.count, out leftovers);
    }

    public ItemContainer Add(int id, int count)
    {
        return Add(id, count, out _);
    }

    public ItemContainer Add(int id, int count, out ItemContainer leftovers)
    {
        return Add(new List<int> { id }, new List<int> { count }, out leftovers);
    }

    public ItemContainer Add(ItemContainer container)
    {
        return Add(container, out _);
    }

    public ItemContainer Add(ItemContainer container, out ItemContainer leftovers)
    {
        if (container == null)
        {
            leftovers = New();
            return this;
        }
        return Add(container.GetIDs(), container.GetCounts(), out leftovers);
    }

    public ItemContainer Add(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        return Add(itemIDAndCounts, out _); 
    }

    public ItemContainer Add(List<AllGameData.ItemIDAndCount> itemIDAndCounts, out ItemContainer leftovers)
    {
        List<int> ids = new();
        List<int> counts = new();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return Add(ids, counts, out leftovers);
    }

    public ItemContainer Add(List<int> ids, List<int> counts)
    {
        return Add(ids, counts, out _);
    }

    public ItemContainer Add(List<int> ids, List<int> counts, out ItemContainer leftovers)
    {
        leftovers = GetLeftOvers(ids, counts);
        if (leftovers.Count() > 0)
        {
            ItemContainer itemsToAdd = leftovers.GetMissing(ids, counts);
            ids = itemsToAdd.GetIDs();
            counts = itemsToAdd.GetCounts();
        }
        for (int i = 0; i < ids.Count; i++)
        {
            int id = ids[i];
            int count = counts[i];
            if (HasItemType(id, out ItemData itemData, true))
            {
                if (AllGameData.itemStackSizes[id] - itemData.count > count)
                {
                    itemData.count += count;
                }
                else
                {
                    ids.Insert(i + 1, id);
                    counts.Insert(i + 1, count - (AllGameData.itemStackSizes[id] - itemData.count));
                    itemData.count = AllGameData.itemStackSizes[id];
                }
            }
            else
            {
                while (count > AllGameData.itemStackSizes[id])
                {
                    inventoryItems.Add(new ItemData(id, AllGameData.itemStackSizes[id]));
                    count -= AllGameData.itemStackSizes[id];
                }
                inventoryItems.Add(new ItemData(id, count));
            }
        }
        ContentChange();
        return this;
    }

    // methonds to remove items
    public ItemContainer Remove(Item item)
    {
        return Remove(item.id, item.count);
    }

    public ItemContainer Remove(int id, int count)
    {
        return Remove(new List<int> { id }, new List<int> { count });
    }

    public ItemContainer Remove(ItemContainer container)
    {
        return Remove(container.GetIDs(), container.GetCounts());
    }

    public ItemContainer Remove(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new();
        List<int> counts = new();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return Remove(ids, counts);
    }

    public ItemContainer Remove(List<int> ids, List<int> counts)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (ids.Contains(invItem.id))
            {
                int index = ids.FindIndex(id => id == invItem.id);
                if (counts[index] > invItem.count)
                {
                    counts[index] -= invItem.count;
                    invItem.count = 0;
                }
                else
                {
                    invItem.count -= counts[index];
                    ids.RemoveAt(index);
                    counts.RemoveAt(index);
                }
            }
        }
        ContentChange();
        return this;
    }

    // methonds to find if inventory is missing items
    public ItemContainer GetMissing(Item item)
    {
        return GetMissing(item.id, item.count);
    }

    public ItemContainer GetMissing(int id, int count)
    {
        return GetMissing(new List<int> { id }, new List<int> { count });
    }

    public ItemContainer GetMissing(ItemContainer container)
    {
        return GetMissing(container.GetIDs(), container.GetCounts());
    }

    public ItemContainer GetMissing(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new();
        List<int> counts = new();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return GetMissing(ids, counts);
    }

    public ItemContainer GetMissing(List<int> ids, List<int> counts)
    {
        return New().Add(ids, counts).Remove(this);
    }

    // methonds to find if inventory has items

    public bool Has(int id, int count)
    {
        return Has(new List<int> { id }, new List<int> { count });
    }

    public bool Has(AllGameData.ItemIDAndCount itemIDAndCount)
    {
        return Has(itemIDAndCount.id, itemIDAndCount.count);
    }

    public bool Has(ItemContainer container)
    {
        return Has(container.GetIDs(), container.GetCounts());
    }

    public bool Has(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new();
        List<int> counts = new();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return Has(ids, counts);
    }

    public bool Has(List<int> ids, List<int> counts)
    {
        return GetMissing(ids, counts).Count() == 0;
    }

    public ItemContainer GetLeftOvers(int id, int count, int emptySlots = -1)
    {
        return GetLeftOvers(new List<int> { id }, new List<int> { count }, emptySlots);
    }

    public ItemContainer GetLeftOvers(AllGameData.ItemIDAndCount itemIDAndCount, int emptySlots = -1)
    {
        return GetLeftOvers(itemIDAndCount.id, itemIDAndCount.count, emptySlots);
    }

    public ItemContainer GetLeftOvers(ItemContainer container, int emptySlots = -1)
    {
        return GetLeftOvers(container.GetIDs(), container.GetCounts(), emptySlots);
    }

    public ItemContainer GetLeftOvers(List<AllGameData.ItemIDAndCount> itemIDAndCounts, int emptySlots = -1)
    {
        List<int> ids = new();
        List<int> counts = new();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return GetLeftOvers(ids, counts, emptySlots);
    }

    public ItemContainer GetLeftOvers(List<int> ids, List<int> counts, int emptySlots = -1)
    {
        if (slotCount == -1) { return New(); }
        ItemContainer leftOversContainer = New();
        if (emptySlots == -1)
        {
            emptySlots = slotCount - inventoryItems.Count();
        }
        for (int i = 0; i < ids.Count(); i++)
        {
            int id = ids[i];
            int count = counts[i];
            if (HasItemType(id, out ItemData itemData, true))
            {
                int leftOvers = count - (AllGameData.itemStackSizes[id] - itemData.count);
                if (leftOvers > 0)
                {
                    leftOversContainer.Add(GetLeftOvers(id, leftOvers, out emptySlots, emptySlots));
                }
            }
            else
            {
                if (emptySlots > 0)
                {
                    emptySlots -= 1;
                    if (count > AllGameData.itemStackSizes[id])
                    {
                        count -= AllGameData.itemStackSizes[id];
                        leftOversContainer.Add(GetLeftOvers(id, count, out emptySlots, emptySlots));
                    }
                }
                else
                {
                    leftOversContainer.Add(id, count);
                }
            }
        }
        return leftOversContainer;
    }

    public ItemContainer GetLeftOvers(int id, int count, out int newEmptySlots, int emptySlots)
    {
        ItemContainer items = GetLeftOvers(new List<int>() { id }, new List<int>() { count }, emptySlots);
        newEmptySlots = emptySlots - (count - items.Count()) / AllGameData.itemStackSizes[id];
        return items;
    }

    public bool HasItemType(int id, out ItemData itemData, bool skipFullStacks = false)
    {
        foreach (ItemData item in inventoryItems)
        {
            if (item.id == id && (item.count < AllGameData.itemStackSizes[id] || !skipFullStacks))
            {
                itemData = item;
                return true;
            }
        }
        itemData = null;
        return false;
    }

    public void Empty()
    {
        inventoryItems = new List<ItemData>();
        ContentChange();
    }

    public void ContentChange()
    {
        List<ItemData> itemsToRemove = new();
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.count == 0)
            {
                itemsToRemove.Add(invItem);
            }
        }
        foreach (ItemData invItem in itemsToRemove)
        {
            inventoryItems.Remove(invItem);
        }
        UpdateAfterContentChange();
    }

    public void UpdateAfterContentChange() { }

    public override string ToString()
    {
        return inventoryItems.ToString();
    }
}