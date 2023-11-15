using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;

public class HotbarAssign : MonoBehaviour
{
    public Hotbar hb;
    void Start()
    {
        hb = GameObject.FindObjectOfType<Hotbar>();
    }
    void OnMouseOver()
    {
        hb.currentlyHovered = gameObject.GetComponent<Button>();
    }
    void OnMouseExit()
    {
        hb.currentlyHovered = null;
    }
}
