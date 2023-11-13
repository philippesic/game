using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ShopPlace : UI
{
    private WorldBlockPlacer worldBlockPlacer;
    private ShopController shop;


    void Start()
    {
        worldBlockPlacer = gameObject.GetComponentInChildren<WorldBlockPlacer>();
        shop = gameObject.GetComponentInParent<ShopController>();
    }

    public void PlaceFactory(int id)
    {
        shop.ToggleShop();
        worldBlockPlacer.StartPlacement(id);
    }

    //This script is attatched to the shop. INstance worldblock placer and do the stuff to instance placing a factory.
    //Each button assign a place function with an id parameter to place correct object
}
