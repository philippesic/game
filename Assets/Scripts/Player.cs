using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemContainer inv;
    public static Player instance;

    private void Awake()
    {
        instance = this;
        inv = ScriptableObject.CreateInstance<ItemContainer>();
    }

    void Update()
    {
        UI.checkForOpenKeys();
    }
}
