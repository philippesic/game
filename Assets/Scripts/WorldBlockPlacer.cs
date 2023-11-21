using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class WorldBlockPlacer : MonoBehaviour
{
    [HideInInspector] public WorldBlock placingBlock;
    [HideInInspector] public int placingBlockID;
    [HideInInspector] public bool isPlacingOnValid = false;
    [HideInInspector] public bool keepPlace = false;
    int blockRotation = 1;
    List<int> rotations = new() { 1, 2, 3, 4, 5, 6 };

    public void StartPlacement(int id, bool keepPlace = false)
    {
        Player.instance.worldBlockBreaker.StopRemoval();
        this.keepPlace = keepPlace;
        IngameUI.instance.SetCrosshairText(0, "Press 'R' To Place " + AllGameData.factoryNames[id]);
        if (placingBlock != null)
        {
            placingBlock.Destroy();
        }
        rotations = AllGameData.factoryRotations.ContainsKey(id) ? AllGameData.factoryRotations[id] : new() { 1, 2, 3, 4, 5, 6 };
        if (blockRotation >= rotations.Count) { blockRotation = 0; }
        placingBlock = CreateShadowObject(id);
        placingBlockID = id;
    }

    public void StopPlacement()
    {
        if (placingBlock != null)
        {
            IngameUI.instance.SetCrosshairText(0);
            placingBlock.Destroy();
            placingBlock = null;
        }
    }

    private bool CheckPlacement(Vector3 pos, int rotation)
    {
        return placingBlock.CanBePlaced() && Player.instance.inv.Has(AllGameData.factoryPlacementCosts[placingBlockID]);
    }

    public bool Place()
    {
        if (placingBlock != null)
        {
            Vector3 pos = WorldBlockContainer.VecToGrid(placingBlock.GetPos());
            if (CheckPlacement(pos, rotations[blockRotation]) && isPlacingOnValid)
            {
                Player.instance.inv.Remove(AllGameData.factoryPlacementCosts[placingBlockID]);
                WorldBlockContainer.instance.CreateBlock(placingBlockID, pos, rotations[blockRotation]);
                if (keepPlace)
                {
                    StartPlacement(placingBlockID, true);
                }
                else
                {
                    StopPlacement();
                }
                return true;
            }
        }
        if (keepPlace)
        {
            StartPlacement(placingBlockID, true);
        }
        else
        {
            StopPlacement();
        }
        return false;
    }

    public WorldBlock CreateShadowObject(int id)
    {
        if (AllGameData.factoryPrefabs.ContainsKey(id))
        {
            WorldBlock shadowObject = Instantiate(AllGameData.factoryPrefabs[id]).GetComponent<WorldBlock>(); ;
            shadowObject.MakeShadow();
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
                placingBlock.SetPos(point, rotations[blockRotation], true);
                return;
            }
        }
        isPlacingOnValid = false;
        placingBlock.SetPos(gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, 10), rotations[blockRotation]);
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
            if (placingBlock != null)
            {
                DoPlacementDisplay();
            }
        }
    }

    private void UpdateRotation()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            blockRotation -= 1;
            if (blockRotation < 0) { blockRotation = rotations.Count-1; }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            blockRotation += 1;
            if (blockRotation >= rotations.Count) { blockRotation = 0; }
        }
    }
}