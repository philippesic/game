using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.tvOS;
using UnityEngine.UIElements;

public class WorldBlockContainer : MonoBehaviour
{
    public List<WorldBlock> blockContainer = new();
    public List<Factory> factoryContainer = new();
    public List<ItemObjectContainingFactory> objectContainingFactoryContainer = new();
    public static int unitsPerGrid = 1;
    public static WorldBlockContainer instance;
    public static Dictionary<int, Vector3> intToRotation = new();

    void Start()
    {
        instance = this;
        intToRotation.Add(1, new Vector3(1, 0, 0));
        intToRotation.Add(2, new Vector3(0, 0, 1));
        intToRotation.Add(3, new Vector3(-1, 0, 0));
        intToRotation.Add(4, new Vector3(0, 0, -1));
        intToRotation.Add(5, new Vector3(0, 1, 0));
        intToRotation.Add(6, new Vector3(0, -1, 0));
    }

    public void CreateBlock(int id, Vector3 pos, int rotation)
    {
        GameObject block = Instantiate(AllGameData.factoryPrefabs[id], pos, RotationIntToRotation3d(rotation), transform);
        WorldBlock blockScript = block.GetComponent<WorldBlock>();
        blockScript.SetPos(pos, rotation, true);
        blockContainer.Add(blockScript);
        if (blockScript.GetBlockFromType(out Factory factory))
        {
            factoryContainer.Add(factory);
            if (blockScript.GetBlockFromType(out ItemObjectContainingFactory objectContainingFactory))
            {
                objectContainingFactoryContainer.Add(objectContainingFactory);
            }
        }
    }

    public void RemoveBlock(WorldBlock block)
    {
        if (blockContainer.Contains(block))
        {
            blockContainer.Remove(block);
        }
        if (block.GetBlockFromType(out Factory factory))
        {
            if (factoryContainer.Contains(factory))
            {
                factoryContainer.Remove(factory);
            }
            if (block.GetBlockFromType(out ItemObjectContainingFactory objectContainingFactory))
            {
                if (objectContainingFactoryContainer.Contains(objectContainingFactory))
                {
                    objectContainingFactoryContainer.Remove(objectContainingFactory);
                }
            }
        }
    }

    public void DoTickUpdate()
    {
        foreach (Factory factory in factoryContainer)
        {
            factory.PreTick();
        }
        DoObjectContainingFactoryUpdate();
        foreach (Factory factory in factoryContainer)
        {
            factory.Tick();
        }
        DoObjectContainingFactoryUpdate();
    }

    List<T> CopyList<T>(List<T> list)
    {
        List<T> newList = new();
        foreach (T item in list)
        {
            newList.Add(item);
        }
        return newList;
    }

    int tick = 0;
    public void DoObjectContainingFactoryUpdate()
    {
        tick++;
        List<ItemObjectContainingFactory> factories = new();
        foreach (ItemObjectContainingFactory factory in objectContainingFactoryContainer)
        {
            if (tick % (factory.ticksPerMove * UpdateTickManager.instance.tickSpeedIncreaseScale) == 0)
            {
                factories.Add(factory);
            }
        }
        if (objectContainingFactoryContainer.Count == factories.Count) { tick = 0; }
        bool didMove;
        do
        {
            didMove = false;
            foreach (ItemObjectContainingFactory factory in CopyList(factories))
            {
                didMove = factory.TryToMoveItem() || didMove;
                if (factory.shouldMoveItems == false)
                {
                    factories.Remove(factory);
                }
            }
        } while (didMove);
    }

    public void DoGeneralUpdate()
    {
        foreach (Factory factory in factoryContainer)
        {
            factory.GeneralUpdate();
        }
    }

    public static Vector3 VecToGrid(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x / unitsPerGrid) * unitsPerGrid,
            Mathf.Round(position.y / unitsPerGrid) * unitsPerGrid,
            Mathf.Round(position.z / unitsPerGrid) * unitsPerGrid
        );
    }

    public static float RotationToGrid(float rotation)
    {
        return Mathf.Round(rotation / 90) * 90;
    }

    public static Quaternion RotationIntToRotation3d(int rotation)
    {
        if (intToRotation.ContainsKey(rotation))
        {
            return Quaternion.LookRotation(intToRotation[rotation], Vector3.up);
        }
        return new Quaternion();
    }
}
