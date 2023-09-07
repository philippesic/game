using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFac : Factory
{
    public override GameObject Create() {
        GameObject newItem = Instantiate(prefab);
        return newItem;
    }
}