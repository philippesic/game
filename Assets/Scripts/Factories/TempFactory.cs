using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFactory : Factory
{
    public override GameObject Create() {
        GameObject newItem = Instantiate(prefab);
        return newItem;
    }
}
