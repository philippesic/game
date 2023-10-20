using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBlockBreaker : MonoBehaviour
{
    public bool isRemoving = false;
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
        if (isRemoving)
        {
            if (Input.GetKey(KeyCode.R))
            {
                WorldBlock lookedAtBlock = PlayerRayCaster.instance.GetLookedAtWorldBlock();
                if (lookedAtBlock != null)
                {
                    lookedAtBlock.Destroy();
                }
            }
        }
    }
}
