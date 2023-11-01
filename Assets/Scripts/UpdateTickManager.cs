using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class UpdateTickManager : MonoBehaviour
{
    public float TickPerSecond;
    public Stopwatch timer;
    public float lastUpdateTime = 0;

    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
        lastUpdateTime = 0;
    }

    void Update()
    {
        while (lastUpdateTime + 1000 / TickPerSecond < timer.ElapsedMilliseconds)
        {
            lastUpdateTime += 1000 / TickPerSecond;
            WorldBlockContainer.instance.DoTickUpdate();
        }
    }
}
