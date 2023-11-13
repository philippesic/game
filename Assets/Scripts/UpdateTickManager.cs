using System.Collections;
using UnityEngine;

public class UpdateTickManager : MonoBehaviour
{
    public float TickPerSecond;
    public System.Diagnostics.Stopwatch timer;
    public float lastUpdateTime = 0;
    public static UpdateTickManager instance;

    void Start()
    {
        instance = this;
        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        lastUpdateTime = 0;
    }

    void Update()
    {
        WorldBlockContainer.instance.DoGeneralUpdate();
        while (lastUpdateTime + 1000 / TickPerSecond < timer.ElapsedMilliseconds)
        {
            lastUpdateTime += 1000 / TickPerSecond;
            WorldBlockContainer.instance.DoTickUpdate();
        }
    }
}
