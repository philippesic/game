
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    public int id;
    public int count;
    [HideInInspector] public ItemObjectContainingFactory containingFactory;
    [HideInInspector] public bool isMoving = false;
    private Vector3 moreToPos;
    private float movementTime;

    public void UpdateMovement()
    {
        if (isMoving)
        {
            
            if (Time.deltaTime / movementTime >= 1)
            {
                isMoving = false;
                transform.position = moreToPos;
            }
            else
            {
                transform.position = transform.position + (moreToPos - transform.position) * (Time.deltaTime / movementTime);
                movementTime -= Time.deltaTime;
            }
        }
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

    public void MoveTo(Vector3 position, int timeTicks = 1)
    {
        Debug.Log(position);
        Debug.Log(transform.position);
        if ((position - transform.position).magnitude < 0.3)
        {
            Debug.Log("MoveTo return");
            transform.position = position;
            return;
        }
        isMoving = true;
        moreToPos = position;
        movementTime = timeTicks / UpdateTickManager.instance.TickPerSecond - Time.deltaTime * 2;

    }

    public void UpdateItemObjectContainingFactory(ItemObjectContainingFactory containingFactory = null)
    {
        this.containingFactory = containingFactory;
    }

    public static GameObject CreateItem(int id, int count = 1)
    {
        GameObject obj = Instantiate(AllGameData.itemPrefabs[id], ItemObjectContainer.instance.transform);
        obj.GetComponent<Item>().count = count;
        return obj;
    }
}
