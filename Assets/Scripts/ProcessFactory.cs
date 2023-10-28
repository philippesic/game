using UnityEngine;
using System.Collections.Generic;

public class ProcessFactory : Factory
{

    public ItemContainer inv;

    void Awake()
    {
        inv = ScriptableObject.CreateInstance<ItemContainer>();
    }

    public override void Tick()
    {
        FindValidRecipes();
    }

    public AllGameData.Recipe FindValidRecipes()
    {
        if (AllGameData.recipes.ContainsKey(blockID))
        {
            foreach (var recipe in AllGameData.recipes[blockID])
            {
                if (!inv.Has(recipe.itemsCost))
                {
                    return recipe;
                }
            }
        }
        return null;
    }
}