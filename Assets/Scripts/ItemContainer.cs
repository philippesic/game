using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using System;

public class ItemContainer : MonoBehaviour
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

    public List<ItemData> inventoryItems = new List<ItemData>();

    public void Add(int id, int count)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.id == id)
            {
                invItem.count += count;
                UpdateAfterContentChange();
                return;
            }
        }

        inventoryItems.Add(new ItemData(id, count));
        UpdateAfterContentChange();
    }

    public void Add(List<int> ids, List<int> counts)
    {
        foreach (var item in ids.Zip(counts, (n, w) => new { id = n, count = w }))
        {
            Add(item.id, item.count);
        }
    }

    public int Remove(int id, int count)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (invItem.id == id)
            {
                invItem.count -= count;
                if (invItem.count < 1)
                {
                    inventoryItems.Remove(invItem);
                    return -invItem.count;
                }

                UpdateAfterContentChange();
                return 0;
            }
        }
        return count;
    }

    public void UpdateAfterContentChange() { }

    public bool HasItems(List<int> ids, List<int> counts)
    {
        foreach (ItemData invItem in inventoryItems)
        {
            if (ids.Contains(invItem.id))
            {
                int index = ids.FindIndex(id => id == invItem.id);
                if (invItem.count >= counts[index])
                {
                    ids.RemoveAt(index);
                    counts.RemoveAt(index);
                }
            }
        }
        return ids.Count == 0;
    }

    internal void Add(string id, int count)
    {
        throw new NotImplementedException();
    }
}
