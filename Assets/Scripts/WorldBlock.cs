using UnityEngine;

public class WorldBlock : MonoBehaviour
{
    public static ItemContainer creationItems = ScriptableObject.CreateInstance<ItemContainer>();

    //public static void

    public static bool CheckPlacement(Vector3 pos, float rotation)
    {
        return /* in can place in location && */ Player.instance.inv.Has(creationItems);
    }

    public static void CreateShadow(Vector3 pos, float rotation) {}

    public static bool Place(Vector3 pos, float rotation)
    {
        if (CheckPlacement(pos, rotation))
        {
            Player.instance.inv.Remove(creationItems);
            // Instantiate stuff
            return true;
        }
        return false;
    }

    public void Destroy() {
        Player.instance.inv.Add(creationItems);
        Destroy(gameObject);
    }
}
