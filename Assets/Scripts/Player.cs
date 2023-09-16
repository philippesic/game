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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponentInChildren<WorldBlockPlacer>().StartPlacement(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.GetComponentInChildren<WorldBlockPlacer>().Place();
        }
    }
}
