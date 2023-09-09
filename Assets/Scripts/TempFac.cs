using UnityEngine;

public class TempFac : Factory
{
    public override GameObject Create() {
        GameObject newItem = Instantiate(prefab);
        return newItem;
    }
}