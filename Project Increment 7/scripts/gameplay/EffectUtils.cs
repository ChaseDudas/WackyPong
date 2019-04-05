using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EffectUtils
{
    #region Properties
    public static bool GetIsSpeedup
    {
        
        get { return SpeedupEffectMonitor.speedup; }
    }

    public static float GetSpeedupFactor
    {
        get { return SpeedupEffectMonitor.speedFactor; }
    }

    public static float GetTimeLeft
    {
        get { return SpeedupEffectMonitor.timeLeft; }
    }
    #endregion

    /// <summary>
    /// Initialize this instance.
    /// </summary>
    public static void Initialize()
    {

    }

}
