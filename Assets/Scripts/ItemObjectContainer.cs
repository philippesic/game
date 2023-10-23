using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectContainer : MonoBehaviour
{
    public static ItemObjectContainer instance;

    void Awake()
    {
        instance = this;
    }
}
