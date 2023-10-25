using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemContainer inv;
    public static Player instance;
    private WorldBlockPlacer worldBlockPlacer;
    private WorldBlockBreaker worldBlockBreaker;

    void Awake()
    {
        instance = this;
        inv = ScriptableObject.CreateInstance<ItemContainer>();
        worldBlockPlacer = gameObject.GetComponentInChildren<WorldBlockPlacer>();
        worldBlockBreaker = gameObject.GetComponentInChildren<WorldBlockBreaker>();
    }

    void Update()
    {
        UI.CheckOpenKeys();
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = int.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 0 && number < 10)
            {
                worldBlockBreaker.StopRemoval();
                worldBlockPlacer.StartPlacement(number);
            }
        }
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < 31; i++)
            {
                Player.instance.inv.Add(i, 1);
            }
            WorldBlock block = PlayerRayCaster.instance.GetLookedAtWorldBlock();
            if (block != null)
            {
                ConveyorFactory blockType = block.GetBlockFromType<ConveyorFactory>();
                if (blockType != null)
                {
                    blockType.GiveItem(new ConveyorFactory.ConveyorFactoryItem(Item.CreateItem(1)));
                }
            }
        }
    }
}
