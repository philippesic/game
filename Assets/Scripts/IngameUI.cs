using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public static IngameUI instance;
    private TextMeshProUGUI crosshairText;
    private Dictionary<int, string> crosshairTextStrings = new();
    void Start()
    {
        instance = this;
        foreach (TextMeshProUGUI textMesh in GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (textMesh.name == "CrosshairText")
            {
                crosshairText = textMesh;
                break;
            }
        }
    }

    public void SetCrosshairText(int key, string text = "")
    {
        if (text == "")
        {
            if (crosshairTextStrings.ContainsKey(key))
        {
            crosshairTextStrings.Remove(key);
        }
        }
        else if (crosshairTextStrings.ContainsKey(key))
        {
            crosshairTextStrings[key] = text;
        }
        else
        {
            crosshairTextStrings.Add(key, text);
        }
        UpdateCrosshairText();
    }

    public void UpdateCrosshairText()
    {
        crosshairText.text = "";
        List<int> indexes = crosshairTextStrings.Keys.ToList();
        indexes.Sort();
        foreach (int index in indexes)
        {
            crosshairText.text += crosshairTextStrings[index] + "\n";
        }
    }
}
