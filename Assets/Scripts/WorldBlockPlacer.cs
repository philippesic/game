using System;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldBlockPlacer : MonoBehaviour
{

    [HideInInspector] public WorldBlock placingBlock;
    [HideInInspector] public int placingBlockID;
    [HideInInspector] public bool isPlacingOnValid = false;

    public void StartPlacement(int id)
    {
        if (placingBlock != null)
        {
            placingBlock.Destroy();
        }
        placingBlock = CreateShadowObject(id);
        placingBlockID = id;
    }

    private bool CheckPlacement(Vector3 pos, float rotation)
    {
        return (
            true &&
            Player.instance.inv.Has(AllGameDate.factoryPlacementCosts[placingBlockID])
        );
    }

    public bool Place(bool keepPlace = true)
    {
        if (placingBlock != null)
        {
            Vector3 pos = WorldBlockContanor.VecToGrid(placingBlock.getPos());
            float rotation = WorldBlockContanor.Rotation2dToGrid(placingBlock.getRotation());
            if (CheckPlacement(pos, rotation) && isPlacingOnValid)
            {
                Player.instance.inv.Remove(AllGameDate.factoryPlacementCosts[placingBlockID]);
                GameObject block = Instantiate(AllGameDate.factoryPrefabs[placingBlockID]);
                block.GetComponent<WorldBlock>().setPos(pos, rotation, true);
                placingBlock.Destroy();
                placingBlock = null;
                if (keepPlace) {
                    StartPlacement(placingBlockID);
                }
                return true;
            }
        }
        return false;
    }

    public WorldBlock CreateShadowObject(int id)
    {
        WorldBlock shadowObject = Instantiate(AllGameDate.factoryPrefabs[id]).GetComponent<WorldBlock>();;
        shadowObject.makeShadow();
        return shadowObject;
    }

    private void doPlacementDisplay()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.distance < 10)
            {
                isPlacingOnValid = true;
                placingBlock.setPos(gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, hitInfo.distance), 0, true);
                return;
            }
        }
        isPlacingOnValid = false;
        placingBlock.setPos(gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, 10), 0);
    }

    public void Update()
    {
        if (placingBlock != null)
        {
            doPlacementDisplay();
        }
    }
}