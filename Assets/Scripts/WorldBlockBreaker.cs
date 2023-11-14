using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
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
        IngameUI.instance.SetCrosshairText(0, "Press 'R' To remove block");
        isRemoving = true;
    }

    public void StopRemoval()
    {
        IngameUI.instance.SetCrosshairText(0);
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
