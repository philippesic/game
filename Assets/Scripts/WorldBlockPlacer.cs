using System;
using UnityEngine;

public class WorldBlockPlacer : MonoBehaviour
{
    [HideInInspector] public WorldBlock placingBlock;
    [HideInInspector] public int placingBlockID;
    [HideInInspector] public bool isPlacingOnValid = false;
    private int blockRotation = 1;
    private float scrollBlockRotation = 1;

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
        WorldBlock shadowObject = Instantiate(AllGameData.factoryPrefabs[id]).GetComponent<WorldBlock>(); ;
        shadowObject.makeShadow();
        return shadowObject;
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
                if (hitInfo.point.x - Math.Floor(hitInfo.point.x) == 0.5f)
                {
                    point = hitInfo.point + new Vector3(0.999f, 0, 0);
                    Vector3 pointOther = hitInfo.point + new Vector3(-0.999f, 0, 0);
                    if ((point - gameObject.transform.position).magnitude > (pointOther - gameObject.transform.position).magnitude)
                    {
                        point = pointOther;
                    }
                }
                else if (hitInfo.point.y - Math.Floor(hitInfo.point.y) == 0.5f)
                {
                    point = hitInfo.point + new Vector3(0, 0.999f, 0);
                    Vector3 pointOther = hitInfo.point + new Vector3(0, -0.999f, 0);
                    if ((point - gameObject.transform.position).magnitude > (pointOther - gameObject.transform.position).magnitude)
                    {
                        point = pointOther;
                    }
                }
                else if (hitInfo.point.z - Math.Floor(hitInfo.point.z) == 0.5f)
                {
                    point = hitInfo.point + new Vector3(0, 0, 0.999f);
                    Vector3 pointOther = hitInfo.point + new Vector3(0, 0, -0.999f);
                    if ((point - gameObject.transform.position).magnitude > (pointOther - gameObject.transform.position).magnitude)
                    {
                        point = pointOther;
                    }
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
        if (placingBlock != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Place();
            }
            DoPlacementDisplay();
            Debug.Log(Input.mouseScrollDelta.y);
            if (Math.Abs(Input.mouseScrollDelta.y) > 0.01)
            {
                scrollBlockRotation -= Input.mouseScrollDelta.y;
                while (scrollBlockRotation > 6.5) {scrollBlockRotation -= 6;}
                while (scrollBlockRotation < 0.5) {scrollBlockRotation += 6;}
                blockRotation = (int) Math.Round(scrollBlockRotation);
            }
            else
            {
                scrollBlockRotation = blockRotation;
            }
        }
    }
}