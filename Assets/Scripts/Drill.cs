using System.Collections;
using System.Security.Principal;
using UnityEditor.Search;
using UnityEngine;

public class Drill : Factory
{
    [HideInInspector] public int itemID;
    [HideInInspector] public float drillSpeedIPT;
    [HideInInspector] public ItemContainer outputItems;
    private float itemsGenerated = 0;

    public override void SetupFactory()
    {
        outputItems = ScriptableObject.CreateInstance<ItemContainer>();

        Collider[] others = Physics.OverlapBox(transform.position, new Vector3(0.9f, 0.9f, 0.9f));
        foreach (Collider other in others)
        {
            if (other.gameObject.TryGetComponent<NodeID>(out var nodeID))
            {
                itemID = nodeID.id;
                drillSpeedIPT = nodeID.drillSpeedIPT;
                break;
            }
        }
    }

    public override void Tick()
    {
        int itemsGeneratedBeforeTick = (int)itemsGenerated;
        itemsGenerated += drillSpeedIPT;
        int itemsGeneratedThisTick = ((int)itemsGenerated) - itemsGeneratedBeforeTick;
        if (itemsGeneratedThisTick > 0)
        {
            if (outputItems.Count() < 60){
                outputItems.Add(itemID, itemsGeneratedThisTick);
            }
            else
            {
                itemsGenerated = 1 - drillSpeedIPT;
            }
        }
            
        if (itemsGenerated % 1 == 0)
        {
            itemsGenerated = 0;
        }
    }
}
