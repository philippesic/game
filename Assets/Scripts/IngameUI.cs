using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public static IngameUI instance;
    private TextMeshProUGUI crosshairText;
    void Start()
    {
        instance = this;
        foreach (TextMeshProUGUI textMesh in GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (textMesh.name == "CrosshairText")
            {
                crosshairText= textMesh;
                break;
            }
        }
    }

    public void SetCrosshairText(string text = "")
    {
        crosshairText.text = text;
    }
}
