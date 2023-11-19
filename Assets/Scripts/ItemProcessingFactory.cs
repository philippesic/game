using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemProssesingFactory : InventoryContainingFactory
{
    [HideInInspector] public ItemContainer input;
    [HideInInspector] public ItemContainer output;
    AllGameData.Recipe currentRecipe;
    ItemContainer inputBuffer;
    int prossesingTicks = 0;

    public override void SetupFactory()
    {
        input = ItemContainer.New(10);
        output = ItemContainer.New(10);
        inputBuffer = ItemContainer.New();
    }

    public override void PreTick()
    {
        input.Add(inputBuffer);
        inputBuffer.Empty();
    }

    public override void Tick()
    {
        TryToMakeItems();
    }

    public override ItemContainer GetExtraBlockCost()
    {
        return input.Copy().Add(output).Add(inputBuffer);
    }

    public override ItemData Get(int count)
    {
        ItemContainer.ItemData itemData = output.Get(count);
        if (itemData == null) { return null; }
        return new ItemData(itemData.id, itemData.count);
    }

    public override bool HasRoomToPush(ItemContainer items)
    {
        return input.GetLeftOvers(inputBuffer.Copy().Add(items)).Count() == 0;
    }

    public override ItemContainer Give(ItemContainer items)
    {
        input.Copy().Add(inputBuffer).Add(items, out ItemContainer leftovers);
        inputBuffer.Add(items).Remove(leftovers);
        return leftovers;
    }

    protected void TryToMakeItems()
    {
        if (currentRecipe != null && input.Has(currentRecipe.itemsCost))
        {
            prossesingTicks += 1;
            if (prossesingTicks >= currentRecipe.ticks)
            {
                input.Remove(currentRecipe.itemsCost);
                output.Add(currentRecipe.itemsMade);
                prossesingTicks = 0;
            }
        }
    }

    public void ChangeCurrentRecipe(AllGameData.Recipe recipe)
    {
        currentRecipe = recipe;
        prossesingTicks = 0;
    }

    public override float GetProssesing0To1()
    {
        return currentRecipe == null ? -1 : prossesingTicks / currentRecipe.ticks;
    }
}
