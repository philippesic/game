using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Hotbar : UI
{
    public Transform HotbarGrid;
    public List<int> HotbarIDs = new();
    public UnityEngine.UI.Button currentlyHovered;
    public static Hotbar instance;
    void Start()
    {
        instance = this;
        for (int i = 0; i < 10; i++)
        {
            HotbarIDs.Add(-1);
        }
        SetGridFactories(HotbarIDs, HotbarGrid);
    }

    void Update()
    {
        if (Input.inputString != "")
        {
            bool is_a_number = int.TryParse(Input.inputString, out int number);
            if (is_a_number && number >= 0 && number < 10 && currentlyHovered != null)
            {
                HotbarIDs[number == 0 ? 9 : number - 1] = AllGameData.factoryIDs[currentlyHovered.GetComponentInChildren<TextMeshProUGUI>().text];
                SetGridFactories(HotbarIDs, HotbarGrid);
            }
        }
    }
}