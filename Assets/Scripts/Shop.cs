using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class Shop : UI
{
    public Transform ShopGrid;

    void Start()
    {
        SetGridFactories(AllGameData.factoryIDsList, ShopGrid);

        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            int id = AllGameData.factoryIDs[btn.GetComponentInChildren<TextMeshProUGUI>().text];
            btn.onClick.AddListener(delegate
            {
                CloseAll();
                Player.instance.worldBlockPlacer.StartPlacement(id, true);
            });
        }
    }
}
