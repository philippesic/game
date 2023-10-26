using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WorldBlockBreaker : MonoBehaviour
{
    public bool isRemoving = false;
    public Stopwatch timer = new();

    void Awake()
    {
        timer.Restart();
    }

    public void StartRemoval()
    {
        isRemoving = true;
    }

    public void StopRemoval()
    {
        isRemoving = false;
    }

    void Update()
    {
        if (isRemoving && Input.GetKey(KeyCode.R) && timer.ElapsedMilliseconds > 100)
        {
            WorldBlock lookedAtBlock = PlayerRayCaster.instance.GetLookedAtWorldBlock();
            if (lookedAtBlock != null)
            {
                lookedAtBlock.Destroy();
                timer.Restart();
            }
        }
    }
}
