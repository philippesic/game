using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public ItemContainer inv;
    [HideInInspector] public static Player instance;
    [HideInInspector] public WorldBlockPlacer worldBlockPlacer;
    [HideInInspector] public WorldBlockBreaker worldBlockBreaker;
    public double mineSpeedMS = 5000.0;
    public List<int> mineIDs = new();
    readonly Stopwatch mineTime = new();
    [HideInInspector] public bool isStopped = false;

    void Awake()
    {
        instance = this;
        inv = ItemContainer.New();
        worldBlockPlacer = gameObject.GetComponentInChildren<WorldBlockPlacer>();
        worldBlockBreaker = gameObject.GetComponentInChildren<WorldBlockBreaker>();
    }

    public void SetPauseState(bool state)
    {
        isStopped = state;
    }

    void Update()
    {
        UIToggle.CheckOpenKeys();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            worldBlockBreaker.StopRemoval();
            worldBlockPlacer.StopPlacement();
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

    void FixedUpdate()
    {
        NodeID node = PlayerRayCaster.instance.GetLookedAtNode();
        IngameUI.instance.SetCrosshairText(1, node == null ? "" : AllGameData.itemNames[node.id]);
        Mine(node);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item))
        {
            item.Pickup(this);
        }
    }

    public void Mine(NodeID node)
    {
        if (node != null)
        {
            if (mineIDs.Contains(node.id))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (!mineTime.IsRunning) { mineTime.Restart(); }
                    IngameUI.instance.SetCrosshairText(10, (int)(mineTime.ElapsedMilliseconds / mineSpeedMS * 100) + "%");
                    ;
                    if (mineTime.ElapsedMilliseconds > mineSpeedMS)
                    {
                        inv.Add(node.id, (int)(node.getNodeMultiplier() * 4));
                        mineTime.Restart();
                    }
                    return;
                }
                else
                {
                    IngameUI.instance.SetCrosshairText(10, "Press 'E' To Mine " + AllGameData.itemNames[node.id]);
                }
            }
            else
            {
                IngameUI.instance.SetCrosshairText(10, "Cannot Mine This");
                ;
            }
        }
        else
        {
            IngameUI.instance.SetCrosshairText(10, "");
        }
        mineTime.Stop();
    }
}
