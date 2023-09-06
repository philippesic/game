using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode inventoryKey;
    public ItemContainer inv;
    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        inv = this.gameObject.AddComponent<ItemContainer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {

        }
    }
}
