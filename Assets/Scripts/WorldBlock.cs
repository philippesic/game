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

    public void makeShadow(){
        isShadow = true;
        GetComponentInChildren<Collider>().isTrigger = true;
    }
    
    public void OnCollisionEnter(Collision collision){
        Debug.Log(collision.gameObject.name);
    }
}
