using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;

using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Shop : UIToggle
{
    public Transform ShopGrid;
    public Hotbar hb;

    void Start()
    {
        hb = FindObjectOfType<Hotbar>();
        SetGridFactories(AllGameData.FactoryIDsList, ShopGrid);

        EventSystem eventSystem = FindObjectOfType<EventSystem>();

        foreach (UnityEngine.UI.Button btn in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            int id = AllGameData.factoryIDs[btn.GetComponentInChildren<TextMeshProUGUI>().text];
            btn.onClick.AddListener(delegate
            {
                CloseAll();
                Player.instance.worldBlockPlacer.StartPlacement(id);
            });

            EventTrigger trigger = btn.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) =>
            {
                hb.currentlyHovered = btn;
            });
            trigger.triggers.Add(entry);

            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener((data) =>
            {
                hb.currentlyHovered = null;
            });
            trigger.triggers.Add(exitEntry);
        }
    }
}
