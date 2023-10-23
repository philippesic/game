using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public int count;
    public ConveyorFactory conveyor;

    public void Pickup()
    {
        Player.instance.inv.Add(id, count);
        if (conveyor != null)
        {
            conveyor.RemoveItemOnbelt();
        }
        Destroy(gameObject);
    }

    public void UpdateConveyor(ConveyorFactory conveyor = null)
    {
        this.conveyor = conveyor;
    }

    public void OnTriggerEnter(Collider other)
    {
        Pickup();
    }

    public static GameObject CreateItem(int id, int count = 1)
    {
        GameObject obj = Instantiate(AllGameData.itemPrefabs[id], ItemObjectContainer.instance.transform);
        obj.GetComponent<Item>().count = count;
        return obj;
    }
}
