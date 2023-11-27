using System.Collections.Generic;
using UnityEngine;

public class WorldBlockContainer : MonoBehaviour
{
    public List<WorldBlock> blockContainer = new List<WorldBlock>();
    public List<Factory> factoryContainer = new List<Factory>();
    public static int unitsPerGrid = 1;
    public static WorldBlockContainer instance;
    public static Dictionary<int, Vector3> intToRotation = new Dictionary<int, Vector3>();

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
        if (block.GetComponent<Factory>())
        {
            factoryContainer.Add(block.GetComponent<Factory>());
        }
    }

    public void RemoveBlock(WorldBlock block)
    {
        if (blockContainer.Contains(block))
        {
            blockContainer.Remove(block);
        }
        Factory factory = block.GetBlockFromType<Factory>();
        if (factory != null)
        {
            if (factoryContainer.Contains(factory))
            {
                factoryContainer.Remove(factory);
            }
        }
    }

    public void DoTickUpdate()
    {
        foreach (Factory factory in factoryContainer)
        {
            factory.PreTick();
        }
        foreach (Factory factory in factoryContainer)
        {
            factory.Tick();
        }
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
