using UnityEngine;

public class Drill : Factory
{
    [HideInInspector] public int itemID;
    public float baseItemsPerTick;
    [HideInInspector] public float nodeMultiplier;
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
                nodeMultiplier = nodeID.getNodeMultiplier();
                return;
            }
        }
        itemID = 0;
        nodeMultiplier = 0;
    }

    public override void Tick()
    {
        int itemsGeneratedBeforeTick = (int)itemsGenerated;
        itemsGenerated += baseItemsPerTick * nodeMultiplier;
        int itemsGeneratedThisTick = ((int)itemsGenerated) - itemsGeneratedBeforeTick;
        if (itemsGeneratedThisTick > 0)
        {
            if (outputItems.Count() < 100){
                outputItems.Add(itemID, itemsGeneratedThisTick);
            }
            else
            {
                itemsGenerated = 1 - (baseItemsPerTick * nodeMultiplier);
            }
        }
        if (itemsGenerated % 1 == 0)
        {
            itemsGenerated = 0;
        }
    }
}
