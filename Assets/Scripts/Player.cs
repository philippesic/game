using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public ItemContainer inv;
    [HideInInspector] public static Player instance;
    [HideInInspector] public WorldBlockPlacer worldBlockPlacer;
    [HideInInspector] public WorldBlockBreaker worldBlockBreaker;

    void Awake()
    {
        instance = this;
        inv = ItemContainer.New();
        worldBlockPlacer = gameObject.GetComponentInChildren<WorldBlockPlacer>();
        worldBlockBreaker = gameObject.GetComponentInChildren<WorldBlockBreaker>();
    }

    void Update()
    {
        PlayerRayCaster.instance.GetLookedAtNode();
        UI.CheckOpenKeys();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            worldBlockBreaker.StopRemoval();
            if (worldBlockPlacer.placingBlock)
            {
                worldBlockPlacer.StopPlacement();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            worldBlockPlacer.StopPlacement();
            if (worldBlockBreaker.isRemoving)
            {
                worldBlockBreaker.StopRemoval();
            }
            else
            {
                worldBlockBreaker.StartRemoval();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item))
        {
            item.Pickup(this);
        }
    }
}
