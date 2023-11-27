
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int count;
    [HideInInspector] public int id = 0;
    [HideInInspector] public ItemObjectContainingFactory containingFactory;
    [HideInInspector] public bool isMoving = false;
    private Vector3 moreToPos;
    private float movementTime;
    public float TimedeltaTimemovementTime;
    public float moreToPostransformposition;

    public void MoveTo(Vector3 position, float movementTimeTicks = 0)
    {
        if (movementTimeTicks <= 1)
        {
            transform.position = position;
        }
        else
        {
            if (moreToPos != null)
            {
                transform.position = moreToPos;
            }
            movementTimeTicks -= 1;
            movementTime = movementTimeTicks / UpdateTickManager.instance.tickPerSecondNoScale - Time.deltaTime;
            moreToPos = position;
            isMoving = true;
        }
    }

    public void UpdateMovement()
    {
        if (isMoving)
        {

            if (movementTime - Time.deltaTime <= 0 || (moreToPos - transform.position).magnitude <= 0.05)
            {
                isMoving = false;
                transform.position = moreToPos;
            }
            else
            {
                transform.position += (moreToPos - transform.position) * (Time.deltaTime / movementTime);
                TimedeltaTimemovementTime = Time.deltaTime / movementTime;
                moreToPostransformposition =  (moreToPos - transform.position).magnitude;
                movementTime -= Time.deltaTime;
            }
        }
    }

    public void UpdateItemObjectContainingFactory(ItemObjectContainingFactory containingFactory = null)
    {
        this.containingFactory = containingFactory;
    }

    public static GameObject CreateItem(int id, int count = 1)
    {
        GameObject obj = Instantiate(AllGameData.itemPrefabs[id], ItemObjectContainer.instance.transform);
        obj.GetComponent<Item>().count = count;
        obj.GetComponent<Item>().id = id;
        return obj;
    }

    public void Pickup(Player player)
    {
        player.inv.Add(Pop());
    }

    public Item Pop()
    {
        if (containingFactory != null)
        {
            containingFactory.RemoveItem(this);
        }
        if (ItemObjectContainingFactory.allItemGameObjectContainersDict.ContainsKey(this))
        {
            Destroy(ItemObjectContainingFactory.allItemGameObjectContainersDict[this].displayObject);
            ItemObjectContainingFactory.allItemGameObjectContainersDict.Remove(this);
        }
        return this;
    }
}
