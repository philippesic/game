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
        Collider[] others = Physics.OverlapBox(transform.position, new Vector3(2f, 2f, 2f)); //, new Quaternion(), new LayerMask(), QueryTriggerInteraction.Collide
        foreach (Collider other in others)
        {
            if (other.gameObject.TryGetComponent(out NodeID nodeID))
            {
                itemID = nodeID.id;
                drillSpeedIPT = nodeID.drillSpeedIPT;
                return;
            }
        }
        itemID = 0;
        drillSpeedIPT = 0;
    }

    public override void Tick()
    {
        int itemsGeneratedBeforeTick = (int)itemsGenerated;
        itemsGenerated += drillSpeedIPT;
        int itemsGeneratedThisTick = ((int)itemsGenerated) - itemsGeneratedBeforeTick;
        if (itemsGeneratedThisTick > 0)
        {
            if (outputItems.Count() < 500){
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
