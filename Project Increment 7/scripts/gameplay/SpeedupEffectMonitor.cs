using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpeedupEffectMonitor : MonoBehaviour
{
    // speedup timer 
    Timer speedupTimer;
    public static bool speedup = false;
    public static float timeLeft = 0.0f;
    public static float speedFactor = ConfigurationUtils.SpeedupFactor;


    void Start()
    {
        //add speedup timer
        print("SpeedupEffectMonitor");
        speedupTimer = gameObject.AddComponent<Timer>();
        speedupTimer.AddTimerFinishedListener(Unspeedup);
        EventManager.AddSpeedupEffectActivatedListener(Speedup);
    }

    void Update()
    {
        if(speedupTimer.Running)
        {
            timeLeft = speedupTimer.GetTime;

        }
        else
        {
            timeLeft = 0.0f;
        }
    }

    /// <summary>
    /// Speedup the specified speedupSide and duration.
    /// </summary>
    /// <param name="speedupSide">Speedup side.</param>
    /// <param name="duration">Duration.</param>
    void Speedup(ScreenSide speedupSide, float duration)
    {
        speedup = true;
        if (!speedupTimer.Running)
        {
            speedupTimer.Duration = duration;
            speedupTimer.Run();
        }
        else
        {
            speedupTimer.AddTime(duration);
        }
    }

    /// <summary>
    /// Reduces speed from balls
    /// </summary>
    void Unspeedup()
    {
        speedup = false;
        speedupTimer.Stop();
    }

}
