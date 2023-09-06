using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class WorldBlock : MonoBehaviour
{
    
    public WorldBlock() {
        
    }

    public void Delete() {
        //InventoryManager.Instance.Add(GetHeldItems());
    }

    List<Item> GetHeldItems() {
        return new List<Item>();
    }
}
