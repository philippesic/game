using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public abstract class UI : MonoBehaviour
{
    public static List<UI> uis = new();

    public GameObject itemObject;

    public void Awake()
    {
        uis.Add(this);
        UIAwake();
        foreach (UI ui in uis)
        {
            ui.gameObject.SetActive(true);
        }
    }

    public virtual void UIAwake() { }

    public void AddItemToGrid(ItemContainer.ItemData item, Transform grid)
    {
        GameObject obj = Instantiate(itemObject, grid);
        MenuItem itemScript = obj.GetComponent<MenuItem>();
        itemScript.SetText(
            AllGameData.itemNames[item.id] +
            (item.count == 1 ? "" : " x" + item.count.ToString())
        );
        itemScript.SetIcon(AllGameData.itemIcons[item.id]);
    }

    public void SetGridItems(List<ItemContainer.ItemData> items, Transform grid)
    {
        foreach (Transform item in grid)
        {
            Destroy(item.gameObject);
        }
        if (items != null)
        {
            foreach (var item in items)
            {
                AddItemToGrid(item, grid);
            }
        }
    }

    public void AddFactoryToGrid(int factoryId, Transform grid)
    {
        GameObject obj = Instantiate(itemObject, grid);
        MenuItem itemScript = obj.GetComponent<MenuItem>();
        if (factoryId != -1)
        {
            itemScript.SetText(AllGameData.factoryNames[factoryId]);
            itemScript.SetIcon(AllGameData.factoryIcons[factoryId]);
        }
        else
        {
            itemScript.SetText("");
        }

    }

    public void SetGridFactories(List<int> factoryIds, Transform grid)
    {
        foreach (Transform item in grid)
        {
            Destroy(item.gameObject);
        }
        if (factoryIds != null)
        {
            foreach (var item in factoryIds)
            {
                AddFactoryToGrid(item, grid);
            }
        }
    }

    public void AddRecipeToGrid(AllGameData.Recipe recipe, Transform grid)
    {
        GameObject obj = Instantiate(itemObject, grid);
        MenuItem itemScript = obj.GetComponent<MenuItem>();
        if (AllGameData.itemIcons.ContainsKey(recipe.itemsMade[0].id))
        {
            itemScript.SetIcon(AllGameData.itemIcons[recipe.itemsMade[0].id]);
        }
        itemScript.SetText(recipe.name);
    }

    public void SetGridRecipes(List<AllGameData.Recipe> recipes, Transform grid)
    {
        foreach (Transform item in grid)
        {
            Destroy(item.gameObject);
        }
        if (recipes != null)
        {
            foreach (var recipe in recipes)
            {
                AddRecipeToGrid(recipe, grid);
            }
        }
    }
}
