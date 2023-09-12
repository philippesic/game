using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public abstract class UI : MonoBehaviour
{
    public static Dictionary<KeyCode, UI> openKeys = new Dictionary<KeyCode, UI>();
    public GameObject itemObject;

    public static void checkForOpenKeys()
    {
        foreach (KeyValuePair<KeyCode, UI> key in UI.openKeys)
        {
            if (Input.GetKeyDown(key.Key))
            {
                key.Value.toggle();
            }
        }
    }

    public KeyCode openKey;
    public bool isOpen = false;

    public void Awake()
    {
        //itemObject = Resources.Load("Assets/Models/2D/Item", typeof(GameObject)) as GameObject;
        openKeys.Add(openKey, this);
        updateVisualState();
    }

    public void toggle()
    {
        isOpen = !isOpen;
        updateVisualState();
    }

    public void setState(bool state = false)
    {
        isOpen = state;
        updateVisualState();
    }

    public void updateVisualState()
    {
        Time.timeScale = isOpen ? 0 : 1;
        gameObject.SetActive(isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }

    public void addItemToGrid(ItemContainer.ItemData item, Transform grid)
    {
        GameObject obj = Instantiate(itemObject, grid);
        MenuItem itemScript = obj.GetComponent<MenuItem>();
        itemScript.setText(
            AllGameDate.itemNames[item.id] +
            (item.count == 1 ? "" : " x" + item.count.ToString())
        );
        itemScript.setIcon(AllGameDate.icons[item.id]);
    }

    public void setGridItems(List<ItemContainer.ItemData> items, Transform grid)
    {
        foreach (Transform item in grid)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            addItemToGrid(item, grid);
        }
    }
}
