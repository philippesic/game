using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemContainer : ScriptableObject
{
    public static ItemContainer New()
    {
        return CreateInstance<ItemContainer>();
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
    }

    public List<ItemData> inventoryItems;

    public void Awake()
    {
        inventoryItems = new List<ItemData>();
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

    public ItemContainer Add(Item item)
    {
        Add(item.id, item.count);
        return this;
    }

    public ItemContainer Add(int id, int count)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.id == id)
            {
                invItem.count += count;
                ContentChange();
                return this;
            }
        }

        inventoryItems.Add(new ItemData(id, count));
        ContentChange();
        return this;
    }

    public ItemContainer Add(List<int> ids, List<int> counts)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (ids.Contains(invItem.id))
            {
                int index = ids.FindIndex(id => id == invItem.id);
                invItem.count += counts[index];
                ids.RemoveAt(index);
                counts.RemoveAt(index);
            }
        }
        for (int i = 0; i < ids.Count; i++)
        {
            inventoryItems.Add(new ItemData(ids[i], counts[i]));
        }
        ContentChange();
        return this;
    }

    public ItemContainer Add(ItemContainer container)
    {
        if (container == null) { return this; }
        Add(container.GetIDs(), container.GetCounts());
        return this;
    }

    public ItemContainer Add(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new();
        List<int> counts = new();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        Add(ids, counts);
        return this;
    }

    public void Remove(int id, int count)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.id == id)
            {
                invItem.count -= count;
                ContentChange();
                return;
            }
        }
    }

    public void Remove(List<int> ids, List<int> counts)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (ids.Contains(invItem.id))
            {
                invItem.count -= counts[ids.FindIndex(id => id == invItem.id)];
            }
        }
        ContentChange();
    }

    public void Remove(ItemContainer container)
    {
        Remove(container.GetIDs(), container.GetCounts());
    }

    public void Remove(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new List<int>();
        List<int> counts = new List<int>();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        Remove(ids, counts);
    }

    public int GetMissing(int id, int count)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.id == id)
            {
                return Math.Max(0, count - invItem.count);
            }
        }
        return count;
    }

    public List<int> GetMissing(List<int> ids, List<int> counts)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (ids.Contains(invItem.id))
            {
                int i = ids.FindIndex(id => id == invItem.id);
                counts[i] = Math.Max(0, counts[i] - invItem.count);
            }
        }
        return counts;
    }

    public List<int> GetMissing(ItemContainer container)
    {
        return GetMissing(container.GetIDs(), container.GetCounts());
    }

    public List<int> GetMissing(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new List<int>();
        List<int> counts = new List<int>();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return GetMissing(ids, counts);
    }

    public bool Has(int id, int count)
    {
        return GetMissing(id, count) == 0;
    }

    public bool Has(AllGameData.ItemIDAndCount itemIDAndCount)
    {
        return GetMissing(itemIDAndCount.id, itemIDAndCount.count) == 0;
    }

    public bool Has(List<int> ids, List<int> counts)
    {
        return GetMissing(ids, counts).All(x => x == 0);
    }

    public bool Has(ItemContainer container)
    {
        return Has(container.GetIDs(), container.GetCounts());
    }

    public bool Has(List<AllGameData.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new List<int>();
        List<int> counts = new List<int>();
        foreach (AllGameData.ItemIDAndCount item in itemIDAndCounts)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return Has(ids, counts);
    }

    public void Empty()
    {
        inventoryItems = new List<ItemData>();
        ContentChange();
    }

    public void ContentChange()
    {
        List<ItemData> itemsToRemove = new List<ItemData>();
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