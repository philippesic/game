using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode inventoryKey;
    public ItemContainer inv = new ItemContainer();
    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {

        }
    }
}
