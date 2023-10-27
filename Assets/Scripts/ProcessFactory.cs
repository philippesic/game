using UnityEngine;
using System.Collections.Generic;
using static AllGameData;

public class ProcessFactory : Factory
{

    public ItemContainer inv;
    public static ProcessFactory instance;
    public int factoryId;
    public Recipe recipe;


    void Awake()
    {
        instance = this;
        inv = ScriptableObject.CreateInstance<ItemContainer>();
        recipe = GetRecipe(factoryId);
    }

    public override void Tick()
    {
        CheckIngredients();
    }

    private void CheckIngredients()
    {
        bool hasAllIngredients = true;
        foreach (var itemIDAndCount in recipe.itemsCost)
        {
            if (!inv.Has(itemIDAndCount))
            {
                hasAllIngredients = false;
                break;
            }
        }
        if (hasAllIngredients)
        {

            inv.Remove(recipe.itemsCost);

            Player.instance.inv.Add(recipe.itemsMade);

        }
    }

    private Recipe GetRecipe(int id)
    {
        foreach (Recipe recipe in recipes)
        {
            if (recipe.machineId == id)
            {
                return recipe;
            }
        }
        return null;
    }
}