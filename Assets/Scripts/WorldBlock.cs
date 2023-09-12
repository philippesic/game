using UnityEngine;

public class WorldBlock : MonoBehaviour
{
    public int blockID;
    public bool isShadow = false;

    public void Destroy()
    {
        if (!isShadow)
        {
            Player.instance.inv.Add(AllGameDate.factoryPlacementCosts[blockID]);
        }
        Destroy(gameObject);
    }

    public void setPos(Vector3 pos, float rotation)
    {
        transform.position = pos;
        transform.rotation = WorldBlockContanor.Rotation2dToRotation3d(rotation);
    }
}
