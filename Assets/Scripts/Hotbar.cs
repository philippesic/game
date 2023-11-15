using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : UI
{
    public Transform HotbarGrid;
    public List<int> HotbarIDs = new();
    public Button currentlyHovered;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            HotbarIDs.Add(-1);
        }
        SetGridFactories(HotbarIDs, HotbarGrid);
    }

    void Update()
    {
        Debug.Log(currentlyHovered);
        if (Input.inputString != "")
        {
            bool is_a_number = int.TryParse(Input.inputString, out int number);
            if (is_a_number && number >= 0 && number < 10 && currentlyHovered != null)
            {
                HotbarIDs[number] = AllGameData.factoryIDs[currentlyHovered.GetComponentInChildren<TextMeshProUGUI>().text];
                SetGridFactories(HotbarIDs, HotbarGrid);
            }
        }
    }
}