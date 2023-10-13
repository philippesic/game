using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemContainer inv;
    public static Player instance;
    private WorldBlockPlacer worldBlockPlacer;

    private void Awake()
    {
        instance = this;
        inv = ScriptableObject.CreateInstance<ItemContainer>();
        worldBlockPlacer = gameObject.GetComponentInChildren<WorldBlockPlacer>();
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
            worldBlockPlacer.Place();
            
        }
    }
}
