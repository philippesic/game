using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectContainingFactory : Factory
{
    public class ItemGameObjectContainer
    {
        public int id;
        public GameObject displayObject;

        public ItemGameObjectContainer(int id)
        {
            this.id = id;
        }

        public ItemGameObjectContainer(GameObject displayObject)
        {
            id = displayObject.GetComponent<Item>().id;
            this.displayObject = displayObject;
        }

        public ItemGameObjectContainer(int id, GameObject displayObject)
        {
            this.id = id;
            this.displayObject = displayObject;
        }
    }

    [HideInInspector] public bool shouldMoveItems = true;

    public void GiveItem(ItemGameObjectContainer item)
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

    public void RemoveItem(ItemGameObjectContainer item)
    {
        if (item.displayObject != null)
        {
            RemoveItem(item.displayObject.GetComponent<Item>());
        }
    }

    public virtual void RemoveItem(Item item) { }

    public virtual bool CanPush(ItemObjectContainingFactory containingFactory)
    {
        return false;
    }

    public virtual bool HasRoomToPush()
    {
        return false;
    }

    public override void PreTick()
    {
        shouldMoveItems = true;
    }

    public void PrintDict(Dictionary<string, Factory> dict)
    {
        foreach (KeyValuePair<string, Factory> pair in dict)
        {
            Debug.Log(pair.Key + pair.Value.ToString());
        }
    }
}
