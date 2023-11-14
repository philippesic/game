using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public abstract class UI : MonoBehaviour
{
    public static List<UI> uis = new();

    public static void CheckOpenKeys()
    {
        foreach (UI ui in uis)
        {
            if (Input.GetKeyDown(ui.openKey))
            {
                CloseAll(ui);
                ui.Toggle();
            }
        }
    }

    public static void CloseAll(UI exeption) { CloseAll(new List<UI>() { exeption }); }

    public static void CloseAll(List<UI> exeptions)
    {
        foreach (UI ui in uis)
        {
            if (!exeptions.Contains(ui))
            {
                ui.SetState();
            }
        }
    }

    public static void CloseAll()
    {
        foreach (UI ui in uis)
        {
            ui.SetState();
        }
    }

    public KeyCode openKey;
    public bool isOpen = false;
    public GameObject itemObject;

    public void Awake()
    {
        uis.Add(this);
        UIAwake();
        UpdateVisualState();
    }

    public virtual void UIAwake() { }

    public void Toggle()
    {
        isOpen = !isOpen;
        UpdateVisualState();
    }

    public void SetState(bool state = false)
    {
        isOpen = state;
        UpdateVisualState();
    }

    public void UpdateVisualState()
    {
        Time.timeScale = isOpen ? 0 : 1;
        gameObject.SetActive(isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
        GetComponent<Image>().enabled = isOpen;
    }

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

        foreach (var item in items)
        {
            AddItemToGrid(item, grid);
        }
    }

    public void AddFactoryToGrid(int factoryId, Transform grid)
    {
        GameObject obj = Instantiate(itemObject, grid);
        MenuItem itemScript = obj.GetComponent<MenuItem>();
        itemScript.SetText(AllGameData.factoryNames[factoryId]);
        itemScript.SetIcon(AllGameData.factoryIcons[factoryId]);
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
