using System;
using System.Collections.Generic;
using UnityEngine;


public class AllItemData : MonoBehaviour
{
    public static Dictionary<int, string> names = new Dictionary<int, string>();
    public static Dictionary<int, string> descriptions = new Dictionary<int, string>();
    public static Dictionary<int, Sprite> icons = new Dictionary<int, Sprite>();
    static AllItemData()
    {
        add(0, "Trash", "t");
        getIcons();
    }

    static void add(int id, string name, string description)
    {
        names.Add(id, name);
        descriptions.Add(id, description);
    }

    static void getIcons()
    {
        foreach (var id in names.Keys)
        {
            icons[id] = Resources.Load("Assets/Item/Icons" + names[id]) as Sprite;
        }
    }

}

