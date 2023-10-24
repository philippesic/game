using System;
using System.Net.Sockets;
using UnityEngine;

public class WorldBlockPlacer : MonoBehaviour
{
    [HideInInspector] public WorldBlock placingBlock;
    [HideInInspector] public int placingBlockID;
    [HideInInspector] public bool isPlacingOnValid = false;
    private int blockRotation = 1;

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

    private bool CheckPlacement(Vector3 pos, int rotation)
    {
        return (
            placingBlock.CanBePlaced() &&
            Player.instance.inv.Has(AllGameData.factoryPlacementCosts[placingBlockID])
        );
    }

    public bool Place(bool keepPlace = true)
    {
        if (placingBlock != null)
        {
            Vector3 pos = WorldBlockContainer.VecToGrid(placingBlock.getPos());
            if (CheckPlacement(pos, blockRotation) && isPlacingOnValid)
            {
                Player.instance.inv.Remove(AllGameData.factoryPlacementCosts[placingBlockID]);
                WorldBlockContainer.instance.CreateBlock(placingBlockID, pos, blockRotation);
                placingBlock.Destroy();
                placingBlock = null;
                if (keepPlace)
                {
                    StartPlacement(placingBlockID);
                }
                return true;
            }
        }
        return false;
    }

    public WorldBlock CreateShadowObject(int id)
    {
        if (AllGameData.factoryPrefabs.ContainsKey(id))
        {
            WorldBlock shadowObject = Instantiate(AllGameData.factoryPrefabs[id]).GetComponent<WorldBlock>(); ;
            shadowObject.makeShadow();
            return shadowObject;
        }
        return null;
    }

    private void DoPlacementDisplay()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.distance < 10)
            {
                Vector3 point = hitInfo.point;
                if (hitInfo.collider.GetComponentInParent<WorldBlock>() != null)
                {
                    point += hitInfo.normal * 0.999f;
                }
                isPlacingOnValid = true;
                placingBlock.setPos(point, blockRotation, true);
                return;
            }
        }
        isPlacingOnValid = false;
        placingBlock.setPos(gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, 10), blockRotation);
    }

    public void Update()
    {
        UpdateRotation();
        if (placingBlock != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Place();
            }
            DoPlacementDisplay();
        }
    }

    private void UpdateRotation()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            blockRotation -= 1;
            if (blockRotation < 1) { blockRotation = 6; }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            blockRotation += 1;
            if (blockRotation > 6) { blockRotation = 1; }
        }
    }
}