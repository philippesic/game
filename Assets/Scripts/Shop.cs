using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Shop : UIToggle
{
    public Transform ShopGrid;

    void Start()
    {
        SetGridFactories(AllGameData.factoryIDsList, ShopGrid);

        foreach (UnityEngine.UI.Button btn in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            // on click action
            int id = AllGameData.factoryIDs[btn.GetComponentInChildren<TextMeshProUGUI>().text];
            btn.onClick.AddListener(delegate
            {
                CloseAll();
                Player.instance.worldBlockPlacer.StartPlacement(id, true);
            });

            // add EventTrigger component to button
            EventTrigger trigger = btn.gameObject.AddComponent<EventTrigger>();

            // PointerEnter
            EventTrigger.Entry entry = new() { eventID = EventTriggerType.PointerEnter };
            entry.callback.AddListener((data) => { Hotbar.instance.currentlyHovered = btn; });
            trigger.triggers.Add(entry);

            // PointerExit
            EventTrigger.Entry exitEntry = new() { eventID = EventTriggerType.PointerExit };
            exitEntry.callback.AddListener((data) => { Hotbar.instance.currentlyHovered = null; });
            trigger.triggers.Add(exitEntry);
        }
    }
}
