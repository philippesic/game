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
    public int inventorySize;

    public override void SetupFactory()
    {
        input = ItemContainer.New();
        output = ItemContainer.New();
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
        return ItemContainer.New().Add(input).Add(output).Add(inputBuffer);
    }

    public override ItemData Get(int count)
    {
        ItemContainer.ItemData itemData = output.Get(count);
        if (itemData == null) { return null; }
        return new ItemData(itemData.id, itemData.count);
    }

    public override bool HasRoomToPush(int count = 1)
    {
        return input.Count() + inputBuffer.Count() + count <= inventorySize;
    }

    public override void Give(ItemData item)
    {
        inputBuffer.Add(item.id, item.count);
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
