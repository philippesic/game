using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeID : MonoBehaviour
{
    public int id;
    [HideInInspector] public float drillSpeedIPT;
    public SpeedTypes multiplierType;

    public void Awake()
    {
        drillSpeedIPT = speedTypeToDrillSpeedIPT[multiplierType];
    }


    public enum SpeedTypes
    {
        Slow,
        Normal,
        Fast
    }

    static Dictionary<SpeedTypes, float> speedTypeToDrillSpeedIPT = new Dictionary<SpeedTypes, float>
    {
        {SpeedTypes.Slow, 0.5f},
        {SpeedTypes.Normal, 1},
        {SpeedTypes.Fast, 2}
    };

    
}
