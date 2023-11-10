using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectContainingFactory : Factory
{
    public static Dictionary<Item, ItemGameObjectContainer> allItemGameObjectContainersDict = new();

    public class ItemGameObjectContainer
    {
        public int id;
        public GameObject displayObject;

        public ItemGameObjectContainer(Item item)
        {
            id = item.id;
            displayObject = item.gameObject;
            allItemGameObjectContainersDict.Add(item, this);
        }

        public ItemGameObjectContainer(int id, Item item)
        {
            this.id = id;
            displayObject = item.gameObject;
            allItemGameObjectContainersDict.Add(item, this);
        }

        public ItemGameObjectContainer(GameObject displayObject)
        {
            id = displayObject.GetComponent<Item>().id;
            this.displayObject = displayObject;
            allItemGameObjectContainersDict.Add(displayObject.GetComponent<Item>(), this);
        }

        public ItemGameObjectContainer(int id, GameObject displayObject)
        {
            this.id = id;
            this.displayObject = displayObject;
            allItemGameObjectContainersDict.Add(displayObject.GetComponent<Item>(), this);
        }

        ~ItemGameObjectContainer()
        {
            allItemGameObjectContainersDict.Remove(displayObject.GetComponent<Item>());
        }
    }

    [HideInInspector] public bool shouldMoveItems = true;

    public void Give(ItemGameObjectContainer item)
    {
        shouldMoveItems = false;
        if (item.displayObject != null)
        {
            item.displayObject.GetComponent<Item>().UpdateItemObjectContainingFactory(this);
            item.displayObject.transform.position = transform.position;
        }
        MannageGivenItem(item);
    }

    protected virtual void MannageGivenItem(ItemGameObjectContainer item) { }

    public ItemGameObjectContainer GetItemGameObjectContainer(int id, int count)
    {
        return GetItemGameObjectContainer(Item.CreateItem(id, count));
    }

    public ItemGameObjectContainer GetItemGameObjectContainer(ItemContainer.ItemData item)
    {
        return GetItemGameObjectContainer(Item.CreateItem(item.id, item.count));
    }

    public ItemGameObjectContainer GetItemGameObjectContainer(Item item, bool createNew = true)
    {
        if (item != null && allItemGameObjectContainersDict.ContainsKey(item))
        {
            return allItemGameObjectContainersDict[item];
        }
        if (createNew)
        {
            return new ItemGameObjectContainer(item);
        }
        return null;
    }

    public ItemGameObjectContainer GetItemGameObjectContainer(GameObject itemObject, bool createNew = true)
    {
        return GetItemGameObjectContainer(itemObject.GetComponent<Item>(), createNew);
    }

    public void RemoveItem(Item item)
    {
        ItemGameObjectContainer itemGameObjectContainer = GetItemGameObjectContainer(item, false);
        if (itemGameObjectContainer != null)
        {
            RemoveItem(itemGameObjectContainer);
        }
    }

    public ItemGameObjectContainer RemoveItem(ItemGameObjectContainer item)
    {
        Item itemClass = item.displayObject.GetComponent<Item>();
        if (itemClass.containingFactory == this)
        {
            itemClass.UpdateItemObjectContainingFactory();
        }
        if (item.displayObject != null && itemClass != null)
        {
            RemoveItemInternal(itemClass);
        }
        return item;
    }

    public virtual bool HasRoomToPush()
    {
        return false;
    }

    protected virtual void RemoveItemInternal(Item item) { }

    public override void PreTick()
    {
        shouldMoveItems = true;
    }
}
