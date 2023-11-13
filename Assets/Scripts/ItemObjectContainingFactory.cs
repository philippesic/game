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
        public Item item;
        public GameObject displayObject;

        public ItemGameObjectContainer(Item item)
        {
            this.item = item;
            id = item.id;
            displayObject = item.gameObject;
            allItemGameObjectContainersDict.Add(item, this);
        }

        public ItemGameObjectContainer(GameObject displayObject)
        {
            item = displayObject.GetComponent<Item>();
            id = item.id;
            this.displayObject = displayObject;
            allItemGameObjectContainersDict.Add(item, this);
        }

        ~ItemGameObjectContainer()
        {
            if (displayObject != null)
            {
                Destroy(displayObject);
            }
            allItemGameObjectContainersDict.Remove(item);
        }
    }

    [HideInInspector] public bool shouldMoveItems = true;

    public void Give(ItemGameObjectContainer item)
    {
        if (item == null) { return; }
        shouldMoveItems = false;
        if (item.displayObject != null)
        {
            item.item.UpdateItemObjectContainingFactory(this);
            item.item.MoveTo(GetOutputPos());
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
        if (item == null) { return null; }
        ItemGameObjectContainer container = GetItemGameObjectContainer(Item.CreateItem(item.id, item.count));
        container.item.transform.position = GetInputPos();
        return container;
    }

    public virtual Vector3 GetInputPos()
    {
        return transform.position;
    }
    
    public virtual Vector3 GetOutputPos()
    {
        return transform.position;
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
        if (item != null)
        {
            ItemGameObjectContainer itemGameObjectContainer = GetItemGameObjectContainer(item, false);
            if (itemGameObjectContainer != null)
            {
                RemoveItem(itemGameObjectContainer);
            }
        }
    }

    public ItemGameObjectContainer RemoveItem(ItemGameObjectContainer item, bool destroyObject = false)
    {
        Item itemClass = item.item;
        if (itemClass.containingFactory == this)
        {
            itemClass.UpdateItemObjectContainingFactory();
        }
        if (item.displayObject != null)
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

    public override void GeneralUpdate()
    {
        UpdateItemMovement();
    }

    public virtual void UpdateItemMovement() {}
}
