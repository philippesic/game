using System.Collections;
using UnityEngine;

public class UpdateTickManager : MonoBehaviour
{
    public float tickPerSecondNoScale;
    public float tickSpeedIncreaseScale = 10;
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

    public float GetTickPerSecond()
    {
        return tickPerSecondNoScale * tickSpeedIncreaseScale;
    }

    void Update()
    {
        UIToggle.DoAllUISleepUpdates();
        WorldBlockContainer.instance.DoGeneralUpdate();
        while (1000 / (tickPerSecondNoScale * tickSpeedIncreaseScale) <= timer.ElapsedMilliseconds + overFlowTime)
        {
            overFlowTime += timer.ElapsedMilliseconds - 1000 / (tickPerSecondNoScale * tickSpeedIncreaseScale);
            timer.Restart();
            WorldBlockContainer.instance.DoTickUpdate();
        }
    }
}
