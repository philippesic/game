using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public ItemContainer inv;
    [HideInInspector] public static Player instance;
    [HideInInspector] public WorldBlockPlacer worldBlockPlacer;
    [HideInInspector] public WorldBlockBreaker worldBlockBreaker;
    public double mineSpeed = 250.0; //divide by 50 tps to get seconds
    public List<int> mineIDs = new();
    private double mineTime = 0.0;
    public bool isStoped = false;

    void Awake()
    {
        instance = this;
        inv = ItemContainer.New();
        worldBlockPlacer = gameObject.GetComponentInChildren<WorldBlockPlacer>();
        worldBlockBreaker = gameObject.GetComponentInChildren<WorldBlockBreaker>();
    }

    public void Stop()
    {
        isStoped = true;
    }

    public void Start()
    {
        isStoped = false;
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
        if (node != null)
        {
            Mine(node);
        }
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
        if (Input.GetKey(KeyCode.E))
        {
            if (mineIDs.Contains(node.id))
            {
                IngameUI.instance.SetCrosshairText(10, Math.Floor(mineTime / (mineSpeed / 100)).ToString() + "%");
                mineTime++;
                if (mineTime == mineSpeed)
                {
                    inv.Add(node.id, 1);
                    mineTime = 0;
                }

            }
            else
            {
                mineTime = 0;
                IngameUI.instance.SetCrosshairText(10, "Cannot Mine This");
            }
        }
    }
}
