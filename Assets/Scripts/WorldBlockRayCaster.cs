using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBlockRayCaster : MonoBehaviour
{
    public static WorldBlockRayCaster instance;

    private void Awake()
    {
        instance = this;
    }

    public WorldBlock GetLookedAtWorldBlock()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.distance < 10 && hitInfo.collider != null)
            {
                if (hitInfo.collider.gameObject.GetComponentInParent<WorldBlock>() != null)
                {
                    return hitInfo.collider.gameObject.GetComponentInParent<WorldBlock>();
                }
            }
        }
        return null;
    }
}
