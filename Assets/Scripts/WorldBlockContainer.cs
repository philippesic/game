using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldBlockContanor : MonoBehaviour
{
    public List<WorldBlock> blockContanor = new List<WorldBlock>();
    public List<WorldBlock> factoryContanor = new List<WorldBlock>();
    public static int unitsPerGrid = 1;
    public static WorldBlockContanor instance;

    void Awake()
    {
        instance = this;
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
