using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemContainer inv;
    public static Player instance;
    private WorldBlockPlacer worldBlockPlacer;
    private WorldBlockBreaker worldBlockBreaker;

    private void Awake()
    {
        instance = this;
        inv = ScriptableObject.CreateInstance<ItemContainer>();
        worldBlockPlacer = gameObject.GetComponentInChildren<WorldBlockPlacer>();
        worldBlockBreaker = gameObject.GetComponentInChildren<WorldBlockBreaker>();
    }

    void Update()
    {
        UI.checkForOpenKeys();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            worldBlockBreaker.StopRemoval();
            if (worldBlockPlacer.placingBlock)
            {
                worldBlockPlacer.StopPlacement();
            }
            else
            {
                worldBlockPlacer.StartPlacement(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            worldBlockPlacer.StopPlacement();
            if (worldBlockBreaker.isRemoving)
            {
                worldBlockBreaker.StopRemoval();
            }
            else
            {
                worldBlockBreaker.StartRemoval();
            }            
        }
        
    }
}
