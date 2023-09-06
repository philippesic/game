using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode inventoryKey;
    public ItemContainer inv;
    public static Player instance;
    public InventoryManager manager;
    public GameObject ui;

    private void Awake()
    {
        instance = this;
        inv = this.gameObject.AddComponent<ItemContainer>();
        manager.ToggleOff(ui);

    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if (ui.activeSelf) {
                manager.ToggleOff(ui);
            }
            else {
                manager.ToggleOn(ui);
            }
    }
}
}
