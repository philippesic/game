using System.Collections.Generic;
using UnityEngine;

public class WorldBlockContanor : ScriptableObject
{
    public static List<WorldBlock> blockContanor = new List<WorldBlock>();
    public static List<WorldBlock> factoryContanor = new List<WorldBlock>();

    public static int unitsPerGrid = 1;

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
}
