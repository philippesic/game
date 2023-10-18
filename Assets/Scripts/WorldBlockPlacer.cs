using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldBlockPlacer : MonoBehaviour
{
    [HideInInspector] public WorldBlock placingBlock;
    [HideInInspector] public int placingBlockID;
    [HideInInspector] public bool isPlacingOnValid = false;
    public GameObject rayPoint;

    public void StartPlacement(int id)
    {
        if (placingBlock != null)
        {
            placingBlock.Destroy();
        }
        placingBlock = CreateShadowObject(id);
        placingBlockID = id;
    }

    public void StopPlacement()
    {
        if (placingBlock != null)
        {
            placingBlock.Destroy();
            placingBlock = null;
        }
    }

    private bool CheckPlacement(Vector3 pos, float rotation)
    {
        return (
            placingBlock.canBePlaced() &&
            Player.instance.inv.Has(AllGameData.factoryPlacementCosts[placingBlockID])
        );
    }

    public bool Place(bool keepPlace = true)
    {
        if (placingBlock != null)
        {
            Vector3 pos = WorldBlockContainer.VecToGrid(placingBlock.getPos());
            float rotation = WorldBlockContainer.Rotation2dToGrid(placingBlock.getRotation());
            if (CheckPlacement(pos, rotation) && isPlacingOnValid)
            {
                Player.instance.inv.Remove(AllGameData.factoryPlacementCosts[placingBlockID]);
                GameObject block = Instantiate(AllGameData.factoryPrefabs[placingBlockID], pos, new Quaternion(), WorldBlockContainer.instance.transform);
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
        WorldBlock shadowObject = Instantiate(AllGameData.factoryPrefabs[id]).GetComponent<WorldBlock>();;
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
                if (rayPoint != null) {
                    rayPoint.transform.position = gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, hitInfo.distance);
                }
                isPlacingOnValid = true;
                placingBlock.setPos(gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, hitInfo.distance), 0, true);
                return;
            }
        }
        isPlacingOnValid = false;
        if (rayPoint != null) {
            rayPoint.transform.position = gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, 10);
        }
        placingBlock.setPos(gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, 10), 0);
    }

    public void Update()
    {
        if (placingBlock != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Place();
            }
            doPlacementDisplay();
        }
    }
}