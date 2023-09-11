using UnityEngine;

public class WorldBlock : MonoBehaviour
{
    public static int blockID;
    public static GameObject prefab;

    public static bool CheckPlacement(Vector3 pos, float rotation)
    {
        return /* in can place in location && */ Player.instance.inv.Has(AllGameDate.FactoryPlacementCost[blockID]);
    }

    public static void CreateShadow(Vector3 pos, float rotation) {
        
    }

    public static bool Place(Vector3 pos, float rotation)
    {
        pos = WorldBlockContanor.VecToGrid(pos);
        rotation = WorldBlockContanor.RotationToGrid(rotation);
        if (CheckPlacement(pos, rotation))
        {
            Player.instance.inv.Remove(AllGameDate.FactoryPlacementCost[blockID]);
            // Instantiate stuff
            return true;
        }
        return false;
    }

    public void Destroy() {
        Player.instance.inv.Add(AllGameDate.FactoryPlacementCost[blockID]);
        Destroy(gameObject);
    }
}
