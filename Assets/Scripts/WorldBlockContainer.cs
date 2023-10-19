using System.Collections.Generic;
using UnityEngine;

public class WorldBlockContainer: MonoBehaviour
{
    public List<WorldBlock> blockContainer = new List<WorldBlock>();
    public List<Factory> factoryContainer = new List<Factory>();
    public static int unitsPerGrid = 1;
    public static WorldBlockContainer instance;
    public Dictionary<char, Vector3> vecToDirection = new Dictionary<char, Vector3>();


    void Awake()
    {
        instance = this;
    }

    public void CreateBlock(int id, Vector3 pos, float rotation)
    {
        GameObject block = Instantiate(AllGameData.factoryPrefabs[id], pos, new Quaternion(), transform);
        WorldBlock blockScript = block.GetComponent<WorldBlock>();
        blockScript.setPos(pos, rotation, true);
        blockContainer.Add(blockScript);
        if (block.GetComponent<Factory>())
        {
            factoryContainer.Add(block.GetComponent<Factory>());
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

    public static Vector3 VecToGrid(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x / unitsPerGrid) * unitsPerGrid,
            Mathf.Round(position.y / unitsPerGrid) * unitsPerGrid,
            Mathf.Round(position.z / unitsPerGrid) * unitsPerGrid
        );
    }

    public static float Rotation2dToGrid(float rotation)
    {
        return Mathf.Round(rotation / 90) * 90;
    }

    public static Quaternion Rotation2dToRotation3d(float rotation)
    {
        Quaternion quat = new Quaternion
        {
            eulerAngles = new Vector3(0, 0, rotation)
        };
        return quat;
    }

    public static float Rotation3dToRotation2d(Quaternion rotation)
    {
        // add later
        return 0;
    }
}
