using System.Collections.Generic;
using UnityEngine;

public class PlayerSubInventory2 : UI
{
    public Transform itemGrid;
    public Transform recipeGrid;
    ItemContainer lastInv;

    public override void UIAwake() { lastInv = ItemContainer.New(); }

    void Update()
    {
        if (!(Player.instance.inv.Has(lastInv) && lastInv.Has(Player.instance.inv)))
        {
            if (itemGrid != null)
            {
                SetGridItems(Player.instance.inv.inventoryItems, itemGrid);
            }
            if (recipeGrid != null)
            {
                List<AllGameData.Recipe> recipes = new();
                foreach (AllGameData.Recipe recipe in AllGameData.playerRecipes)
                {
                    if (Player.instance.inv.Has(recipe.itemsCost))
                    {
                        recipes.Add(recipe);
                    }
                }
                SetGridRecipes(recipes, recipeGrid);
                foreach (UnityEngine.UI.Button button in recipeGrid.GetComponentsInChildren<UnityEngine.UI.Button>())
                {
                    AllGameData.Recipe recipe = AllGameData.recipeNames[button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text];
                    button.onClick.AddListener(delegate
                    {
                        TryCraft(recipe);
                    });
                }
            }
        }
        lastInv = ItemContainer.New();
        lastInv.Add(Player.instance.inv);
    }

    public void TryCraft(AllGameData.Recipe recipe)
    {
        if (Player.instance.inv.Has(recipe.itemsCost))
        {
            ItemContainer inv = Player.instance.inv.Copy();
            Player.instance.inv.Remove(recipe.itemsCost);
            Player.instance.inv.Add(recipe.itemsMade, out ItemContainer leftovers);
            if (!leftovers.IsEmpty())
            {
                Player.instance.inv = inv;
            }

        }
    }
}