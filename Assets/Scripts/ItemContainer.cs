using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemContainer : ScriptableObject
{
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

    public List<int> getIDs()
    {
        List<int> ids = new List<int>();
        foreach (ItemData item in inventoryItems)
        {
            ids.Add(item.id);
        }
        return ids;
    }

    public List<int> getCounts()
    {
        List<int> counts = new List<int>();
        foreach (ItemData item in inventoryItems)
        {
            counts.Add(item.count);
        }
        return counts;
    }

    public void Add(int id, int count)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.id == id)
            {
                invItem.count += count;
                ContentChange();
                return;
            }
        }

        inventoryItems.Add(new ItemData(id, count));
        ContentChange();
    }

    public void Add(List<int> ids, List<int> counts)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (ids.Contains(invItem.id))
            {
                invItem.count += counts[ids.FindIndex(id => id == invItem.id)];
            }
        }
        ContentChange();
    }

    public void Add(ItemContainer container)
    {
        Add(container.getIDs(), container.getCounts());
    }

    public void Add(List<AllGameDate.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new List<int>();
        List<int> counts = new List<int>();
        foreach (ItemData item in inventoryItems)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        Add(ids, counts);
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
        Remove(container.getIDs(), container.getCounts());
    }

    public void Remove(List<AllGameDate.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new List<int>();
        List<int> counts = new List<int>();
        foreach (ItemData item in inventoryItems)
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
        return GetMissing(container.getIDs(), container.getCounts());
    }

    public List<int> GetMissing(List<AllGameDate.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new List<int>();
        List<int> counts = new List<int>();
        foreach (ItemData item in inventoryItems)
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

    public bool Has(List<int> ids, List<int> counts)
    {
        return GetMissing(ids, counts).All(x => x == 0);
    }

    public bool Has(ItemContainer container)
    {
        return Has(container.getIDs(), container.getCounts());
    }

    public bool Has(List<AllGameDate.ItemIDAndCount> itemIDAndCounts)
    {
        List<int> ids = new List<int>();
        List<int> counts = new List<int>();
        foreach (ItemData item in inventoryItems)
        {
            ids.Add(item.id);
            counts.Add(item.count);
        }
        return Has(ids, counts);
    }

    internal void Add(string id, int count)
    {
        throw new NotImplementedException();
    }

    public void ContentChange() {
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.count == 0)
            {
                inventoryItems.Remove(invItem);
            }
        }
        UpdateAfterContentChange();
    }

    public void UpdateAfterContentChange() { }

}
