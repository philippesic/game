using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemContainer inv;
    public static Player instance;

    private void Awake()
    {
        instance = this;
        inv = this.gameObject.AddComponent<ItemContainer>();
    }

    void Update()
    {
        UI.checkForOpenKeys();
    }
}
