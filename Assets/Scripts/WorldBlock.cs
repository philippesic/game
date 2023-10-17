using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldBlock : MonoBehaviour
{
    public int blockID;
    public bool isShadow = false;
    public bool isDestroyed = false;

    public void Destroy()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            GetDestroyed();
            if (!isShadow)
            {
                Player.instance.inv.Add(AllGameDate.factoryPlacementCosts[blockID]);
            }
            Destroy(gameObject);
        }
    }
    protected virtual void GetDestroyed() { }

    public void setPos(Vector3 pos, float rotation, bool onGrid = false)
    {
        transform.position = onGrid ? WorldBlockContanor.VecToGrid(pos) : pos;
        transform.rotation = WorldBlockContanor.Rotation2dToRotation3d(rotation);
    }

    public Vector3 getPos()
    {
        return transform.position;
    }

    public float getRotation()
    {
        return WorldBlockContanor.Rotation3dToRotation2d(transform.rotation);
    }

    public void makeShadow()
    {
        isShadow = true;
        GetComponentInChildren<Collider>().isTrigger = true;
    }

    public List<Collider> getCurrentBlockCollisions()
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

    public bool canBePlaced()
    {
        return getCurrentBlockCollisions().Count <= 0;
    }
}
