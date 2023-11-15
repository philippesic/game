using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldBlock : MonoBehaviour
{
    public int blockID;
    [HideInInspector] public bool isShadow = false;
    [HideInInspector] public bool isDestroyed = false;

    public void Destroy()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            if (!isShadow)
            {
                Player.instance.inv.Add(AllGameData.factoryPlacementCosts[blockID]);
                Player.instance.inv.Add(GetExtraBlockCost());
            }
            GetDestroyed();
            WorldBlockContainer.instance.RemoveBlock(this);
            Destroy(gameObject);
        }
    }

    public virtual ItemContainer GetExtraBlockCost()
    {
        return null;
    }
    protected virtual void GetDestroyed() { }

    public void SetPos(Vector3 pos, int rotation, bool onGrid = false)
    {
        transform.position = onGrid ? WorldBlockContainer.VecToGrid(pos) : pos;
        transform.rotation = WorldBlockContainer.RotationIntToRotation3d(rotation);
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public void MakeShadow()
    {
        isShadow = true;
        foreach (Collider collider in GetComponentsInChildren<Collider>()) { collider.isTrigger = true; }
    }

    public List<Collider> GetCurrentBlockCollisions()
    {
        Collider[] collisions = Physics.OverlapBox(transform.position, new Vector3(0.49f, 0.49f, 0.49f));
        List<Collider> blockCollisions = new List<Collider>();
        foreach (Collider collision in collisions)
        {
            if (collision.GetComponentInParent<WorldBlock>() != null || collision.GetComponentInParent<Player>() != null)
            {
                blockCollisions.Add(collision);
            }
        }
        return blockCollisions;
    }

    public bool CanBePlaced()
    {
        return GetCurrentBlockCollisions().Count <= 0;
    }

    public T GetBlockFromType<T>() where T : WorldBlock
    {
        return GetComponentInParent<T>();
    }
}
