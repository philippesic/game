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
        itemScript.setText(
            AllGameData.itemNames[item.id] +
            (item.count == 1 ? "" : " x" + item.count.ToString())
        );
        itemScript.setIcon(AllGameData.itemIcons[item.id]);
    }

    public void SetGridItems(List<ItemContainer.ItemData> items, Transform grid)
    {
        foreach (Transform item in grid)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            AddItemToGrid(item, grid);
        }
    }

    public void AddFactoryToGrid(int factoryId, Transform grid)
    {
        GameObject obj = Instantiate(itemObject, grid);
        MenuItem itemScript = obj.GetComponent<MenuItem>();
        if (AllGameData.FactoryIDsList.Contains(factoryId))
        {
            itemScript.setText(AllGameData.factoryNames[factoryId]);
            itemScript.setIcon(AllGameData.factoryIcons[factoryId]);
        }
    }

    public void SetGridFactories(List<int> factoryIds, Transform grid)
    {
        foreach (Transform item in grid)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in factoryIds)
        {
            AddFactoryToGrid(item, grid);
        }
    }
}
