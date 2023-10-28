
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public int count;
    public ItemObjectContainingFactory containingFactory;

    public void Pickup(Player player)
    {
        player.inv.Add(id, count);
        if (containingFactory != null)
        {
            containingFactory.RemoveItem(this);
        }
        Destroy(gameObject);
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
