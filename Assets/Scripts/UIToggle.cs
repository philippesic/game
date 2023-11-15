using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq.Expressions;

public class UIToggle : UI
{
    public static new List<UIToggle> uis = new();

    new void Awake()
    {
        uis.Add(this);
        UIAwake();
        foreach (UIToggle ui in uis)
        {
            ui.gameObject.SetActive(false);
        }
        UpdateVisualState();
    }
    public static void CheckOpenKeys()
    {
        foreach (UIToggle ui in uis)
        {
            if (Input.GetKeyDown(ui.openKey))
            {
                CloseAll(new List<UIToggle> { ui });
                ui.Toggle();
            }
        }
    }

    public static void CloseAll(UIToggle exeption) { CloseAll(new List<UIToggle>() { exeption }); }

    public static void CloseAll(List<UIToggle> exeptions)
    {
        foreach (UIToggle ui in uis)
        {
            if (!exeptions.Contains(ui))
            {
                ui.SetState();
            }
        }
    }

    public static void CloseAll()
    {
        foreach (UIToggle ui in uis)
        {
            ui.SetState();
        }
    }

    public KeyCode openKey;
    public bool isOpen = false;

    public void Toggle()
    {
        isOpen = !isOpen;
        Player.instance.uiOpen = isOpen;
        UpdateVisualState();
    }

    public void SetState(bool state = false)
    {
        isOpen = state;
        Player.instance.uiOpen = isOpen;
        UpdateVisualState();
    }

    public void UpdateVisualState()
    {
        Time.timeScale = isOpen ? 0 : 1;
        gameObject.SetActive(isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
        GetComponent<Image>().enabled = isOpen;
    }
}