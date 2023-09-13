using UnityEngine;
using UnityEngine.UIElements;

public class WorldBlockPlacer : MonoBehaviour
{

    [HideInInspector] public WorldBlock placingBlock;
    [HideInInspector] public int placingBlockID;

    public void StartPlacement(int id)
    {
        if (placingBlock != null)
        {
            placingBlock.Destroy();
        }
        placingBlock = CreateShadowObject(id).GetComponent<WorldBlock>();
        placingBlockID = id;
    }

    private bool CheckPlacement(Vector3 pos, float rotation)
    {
        return (
            /* in can place in location && */
            Player.instance.inv.Has(AllGameDate.factoryPlacementCosts[placingBlockID])
        );
    }

    public bool Place(Vector3 pos, float rotation)
    {
        pos = WorldBlockContanor.VecToGrid(pos);
        rotation = WorldBlockContanor.Rotation2dToGrid(rotation);
        if (CheckPlacement(pos, rotation))
        {
            Player.instance.inv.Remove(AllGameDate.factoryPlacementCosts[placingBlockID]);
            GameObject block = Instantiate(AllGameDate.factoryPrefabs[placingBlockID]);
            block.GetComponent<WorldBlock>().setPos(pos, rotation, true);
            placingBlock.Destroy();
            placingBlock = null;
            return true;
        }
        return false;
    }

    public GameObject CreateShadowObject(int id)
    {
        GameObject shadowObject = Instantiate(AllGameDate.factoryPrefabs[id]);
        shadowObject.GetComponent<WorldBlock>().isShadow = true;
        foreach(Collider collider in shadowObject.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
        return shadowObject;
    }

    private void doPlacementDisplay()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo)) {
            if (hitInfo.distance < 10)
            {
                placingBlock.setPos(gameObject.transform.position + gameObject.transform.rotation * new Vector3(0, 0, hitInfo.distance), 0, true);
                return;
            }
        }
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