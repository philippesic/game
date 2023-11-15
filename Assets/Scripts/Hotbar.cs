using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Hotbar : UI
{
    public Transform hotbarGrid;
    public List<int> hotbarIDs = new();
    public UnityEngine.UI.Button currentlyHovered;
    public static Hotbar instance;
    void Start()
    {
        instance = this;
        for (int i = 0; i < 10; i++)
        {
            hotbarIDs.Add(-1);
        }
        SetGridFactories(hotbarIDs, hotbarGrid);
    }

    void Update()
    {
        if (Input.inputString != "")
        {
            bool is_a_number = int.TryParse(Input.inputString, out int number);
            if (is_a_number && number >= 0 && number < 10 && currentlyHovered != null)
            {
                if (UIToggle.uiIsOpen)
                {
                    hotbarIDs[number == 0 ? 9 : number - 1] = AllGameData.factoryIDs[currentlyHovered.GetComponentInChildren<TextMeshProUGUI>().text];
                    SetGridFactories(hotbarIDs, hotbarGrid);
                }
                else
                {
                    if (hotbarIDs[number == 0 ? 9 : number - 1] == -1)
                    {
                        Player.instance.worldBlockPlacer.StopPlacement();
                    }
                    else
                    {
                        Player.instance.worldBlockPlacer.StartPlacement(hotbarIDs[number == 0 ? 9 : number - 1], true);
                    }
                }
            }
        }
    }
}