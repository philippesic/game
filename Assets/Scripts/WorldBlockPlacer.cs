using System.Collections.Generic;
using UnityEngine;
public class WorldBlockPlacer : ScriptableObject
{
    public static WorldBlock placingBlock;
    public static int placingBlockID;

    public static void StartPlacement(int id)
    {
        placingBlock = CreateShadowObject(id).GetComponent<WorldBlock>();
    }

    public static bool CheckPlacement(Vector3 pos, float rotation)
    {
        return (
            /* in can place in location && */
            Player.instance.inv.Has(AllGameDate.factoryPlacementCosts[placingBlock.blockID])
        );
    }

    public static bool Place(Vector3 pos, float rotation)
    {
        pos = WorldBlockContanor.VecToGrid(pos);
        rotation = WorldBlockContanor.Rotation2dToGrid(rotation);
        if (CheckPlacement(pos, rotation))
        {
            Player.instance.inv.Remove(AllGameDate.factoryPlacementCosts[placingBlock.blockID]);
            GameObject block = Instantiate(AllGameDate.factoryPrefabs[placingBlock.blockID]);
            block.GetComponent<WorldBlock>().setPos(pos, rotation);
            placingBlock.Destroy();
            placingBlock = null;
            return true;
        }
        return false;
    }

    public static GameObject CreateShadowObject(int id)
    {
        Debug.Log(AllGameDate.factoryPrefabs[0].gameObject);
        GameObject shadowObject = Instantiate(AllGameDate.factoryPrefabs[id]);
        shadowObject.GetComponent<WorldBlock>().isShadow = true;
        return shadowObject;
    }

    public void Update()
    {
        if (placingBlock == null)
        {
            placingBlock.setPos(Player.instance.gameObject.transform.position + new Vector3(0, 0, 10), 0);
        }
    }
}