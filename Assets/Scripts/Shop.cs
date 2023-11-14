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
        SetGridFactories(AllGameData.FactoryIDsList, ShopGrid);

        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            int id = AllGameData.factoryIDs[btn.GetComponentInChildren<TextMeshProUGUI>().text];
            btn.onClick.AddListener(delegate
            {
                UI.CloseAll();
                PlaceFactory(id);
            });
        }
    }

    private void PlaceFactory(int id)
    {
        Player.instance.worldBlockPlacer.StartPlacement(id);
    }

    //This script is attatched to the shop. INstance worldblock placer and do the stuff to instance placing a factory.
    //Each button assign a place function with an id parameter to place correct object
}
