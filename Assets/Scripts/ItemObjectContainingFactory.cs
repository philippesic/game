using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObjectContainingFactory : Factory
{
    public static Dictionary<Item, ItemGameObjectContainer> allItemGameObjectContainersDict = new();

    public ItemGameObjectContainer GetItemGameObjectContainer(ItemContainer.ItemData item)
    {
        if (item == null) { return null; }
        return GetItemGameObjectContainer(item.id, item.count);
    }

    public ItemGameObjectContainer GetItemGameObjectContainer(int id, int count)
    {
        return GetItemGameObjectContainer(Item.CreateItem(id, count));
    }

    public ItemGameObjectContainer GetItemGameObjectContainer(GameObject itemObject, bool createNew = true)
    {
        if (itemObject == null) { return null; }
        return GetItemGameObjectContainer(itemObject.GetComponent<Item>(), createNew);
    }

    public ItemGameObjectContainer GetItemGameObjectContainer(Item item, bool createNew = true)
    {
        if (item != null && allItemGameObjectContainersDict.ContainsKey(item))
        {
            return allItemGameObjectContainersDict[item];
        }
        if (createNew)
        {
            ItemGameObjectContainer container = new(item);
            container.item.MoveTo(GetInputPos());
            return container;
        }
        return null;
    }


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

    public bool shouldMoveItems = true;
    public int ticksPerMove = 3;

    public void Give(ItemGameObjectContainer item)
    {
        // check null and destroyed(
        if (item == null) { return; }
        if (item.item == null) { return; }
        if (item.item.IsDestroyed()) { return; }
        // check if incomplete
        if (item.displayObject == null)
        {
            item = GetItemGameObjectContainer(item.item);
        }
        // recive item
        shouldMoveItems = false;
        item.item.UpdateItemObjectContainingFactory(this);
        item.item.MoveTo(GetOutputPos(), ticksPerMove);
        MannageGivenItem(item);
    }

    protected virtual void MannageGivenItem(ItemGameObjectContainer item) { }

    public virtual Vector3 GetInputPos()
    {
        return transform.position;
    }

    public virtual Vector3 GetOutputPos()
    {
        return transform.position;
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

    public ItemGameObjectContainer RemoveItem(ItemGameObjectContainer item)
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

    public virtual bool TryToMoveItem()
    {
        shouldMoveItems = false;
        return false;
    }

    public override void GeneralUpdate()
    {
        UpdateItemMovement();
    }

    public virtual void UpdateItemMovement() { }
}
