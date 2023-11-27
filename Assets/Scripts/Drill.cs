using System;
using System.Diagnostics;
using UnityEngine;

public class Drill : InventoryContainingFactory
{
    [HideInInspector] public int itemID;
    public float baseItemsPerTick;
    public int inventorySize;
    [HideInInspector] public float nodeMultiplier;
    [HideInInspector] public ItemContainer outputItems;
    readonly Stopwatch stopwatch = new();
    float itemsGenerated = 0;

    public override void SetupFactory()
    {
        outputItems = ItemContainer.New();
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
        stopwatch.Start();
    }

    public override ItemContainer GetExtraBlockCost()
    {
        return outputItems;
    }

    public override ItemData Get(int count)
    {
        ItemContainer.ItemData itemData = outputItems.Get(count);
        if (itemData == null) { return null; }
        return new ItemData(itemData.id, itemData.count);
    }

    public override void Tick()
    {
        int itemsGeneratedBeforeTick = (int)itemsGenerated;
        itemsGenerated += baseItemsPerTick * nodeMultiplier / UpdateTickManager.instance.tickSpeedIncreaseScale;
        int itemsGeneratedThisTick = ((int)itemsGenerated) - itemsGeneratedBeforeTick;
        if (itemsGeneratedThisTick > 0)
        {
            if (outputItems.Count() < inventorySize){
                stopwatch.Restart();
                outputItems.Add(itemID, itemsGeneratedThisTick);
            }
            else
            {
                itemsGenerated = 1 - (baseItemsPerTick * nodeMultiplier / UpdateTickManager.instance.tickSpeedIncreaseScale);
            }
        }
        if (itemsGenerated % 1 == 0)
        {
            itemsGenerated = 0;
        }
    }

    public override float GetProssesing0To1()
    {
        return Math.Clamp(stopwatch.ElapsedMilliseconds / 1000 * UpdateTickManager.instance.GetTickPerSecond() / (1 / baseItemsPerTick * nodeMultiplier / UpdateTickManager.instance.tickSpeedIncreaseScale), 0, 1);
    }
}
