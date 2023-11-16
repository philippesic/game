using System.Collections;
using UnityEngine;

public class UpdateTickManager : MonoBehaviour
{
    public float TickPerSecond;
    public System.Diagnostics.Stopwatch timer;
    public float overFlowTime = 0;
    public static UpdateTickManager instance;

    void Start()
    {
        instance = this;
        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        overFlowTime = 0;
    }

    void Update()
    {
        UIToggle.DoAllUISleepUpdates();
        WorldBlockContainer.instance.DoGeneralUpdate();
        while (1000 / TickPerSecond <= timer.ElapsedMilliseconds + overFlowTime)
        {
            overFlowTime += timer.ElapsedMilliseconds - 1000 / TickPerSecond;
            timer.Restart();
            WorldBlockContainer.instance.DoTickUpdate();
        }
    }
}
