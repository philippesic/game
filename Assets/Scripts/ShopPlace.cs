using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ShopPlace : MonoBehaviour
{
    public GameObject prefab;
    public void Awake() {
        GameObject placer = new GameObject("Placer");

        
        FactPlaceTest script = placer.AddComponent<FactPlaceTest>();

        if (script != null)
        {
            script.factory = Instantiate(prefab);
            script.placer = placer;
            script.grid = FindObjectOfType<SnapGrid>();
            
        }
        else
        {
            Debug.LogWarning("Not Found");
        }

        
        Selection.activeObject = placer;
    }
}
