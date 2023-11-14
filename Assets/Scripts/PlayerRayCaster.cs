using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCaster : MonoBehaviour
{
    public static PlayerRayCaster instance;

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
                // Block
                if (hitInfo.collider.gameObject.GetComponentInParent<WorldBlock>() != null)
                {
                    return hitInfo.collider.gameObject.GetComponentInParent<WorldBlock>();
                }

            }
        }
        return null;
    }

    public void GetLookedAtNode()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.distance < 3 && hitInfo.collider != null)
            {
                // Node
                if (hitInfo.collider.gameObject.GetComponent<NodeID>() != null)
                {
                    IngameUI.instance.SetCrosshairText(AllGameData.itemNames[hitInfo.collider.gameObject.GetComponent<NodeID>().id]);
                }
            }
            else
            {
                IngameUI.instance.SetCrosshairText("");
            }
        }
    }
}
