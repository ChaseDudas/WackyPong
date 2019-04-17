using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The event manager
/// </summary>
public static class EventManager
{
    #region Fields

    // ball lost support
    static List<Ball> ballLostInvokers = new List<Ball>();
    static List<UnityAction<ScreenSide, int>> ballLostListeners =
        new List<UnityAction<ScreenSide, int>>();

    // ball died support
    static List<Ball> ballDiedInvokers = new List<Ball>();
    static List<UnityAction> ballDiedListeners =
        new List<UnityAction>();

    // hits added support
    static List<Paddle> hitsAddedInvokers = new List<Paddle>();
    static List<UnityAction<ScreenSide, int>> hitsAddedListeners =
        new List<UnityAction<ScreenSide, int>>();

    // freezer effect support
    static List<Pickup> freezerEffectActivatedInvokers = new List<Pickup>();
    static List<UnityAction<ScreenSide, float>> freezerEffectActivatedListeners =
        new List<UnityAction<ScreenSide, float>>();

    // speedup effect support
    static List<Pickup> speedupEffectActivatedInvokers = new List<Pickup>();
    static List<UnityAction<float, float>> speedupEffectActivatedListeners =
        new List<UnityAction<float, float>>();

    // player won event support
    static List<HUD> playerWonInvokers = new List<HUD>();
    static List<UnityAction<ScreenSide>> playerWonListeners =
        new List<UnityAction<ScreenSide>>();

    #endregion

    #region Ball lost support

    /// <summary>
    /// Adds the given script as a ball lost invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddBallLostInvoker(Ball invoker)
    {
        ballLostInvokers.Add(invoker);
        foreach (UnityAction<ScreenSide, int> listener in ballLostListeners)
        {
            invoker.AddBallLostListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a ball lost listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddBallLostListener(UnityAction<ScreenSide, int> listener)
    {
        ballLostListeners.Add(listener);
        foreach (Ball invoker in ballLostInvokers)
        {
            invoker.AddBallLostListener(listener);
        }
    }

    #endregion

    #region Ball died support

    /// <summary>
    /// Adds the given script as a ball died invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddBallDiedInvoker(Ball invoker)
    {
        ballDiedInvokers.Add(invoker);
        foreach (UnityAction listener in ballDiedListeners)
        {
            invoker.AddBallDiedListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a ball died listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddBallDiedListener(UnityAction listener)
    {
        ballDiedListeners.Add(listener);
        foreach (Ball invoker in ballDiedInvokers)
        {
            invoker.AddBallDiedListener(listener);
        }
    }

    #endregion

    #region Hits added support

    /// <summary>
    /// Adds the given script as a hits added invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddHitsAddedInvoker(Paddle invoker)
    {
        hitsAddedInvokers.Add(invoker);
        foreach (UnityAction<ScreenSide, int> listener in hitsAddedListeners)
        {
            invoker.AddHitsAddedListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a hits added listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddHitsAddedListener(UnityAction<ScreenSide, int> listener)
    {
        hitsAddedListeners.Add(listener);
        foreach (Paddle invoker in hitsAddedInvokers)
        {
            invoker.AddHitsAddedListener(listener);
        }
    }

    #endregion

    #region Freezer effect activated support

    /// <summary>
    /// Adds the given script as a freezer effect activated invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddFreezerEffectActivatedInvoker(Pickup invoker)
    {
        freezerEffectActivatedInvokers.Add(invoker);
        foreach (UnityAction<ScreenSide, float> listener in freezerEffectActivatedListeners)
        {
            invoker.AddFreezerEffectActivatedListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a freezer effect activated listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddFreezerEffectActivatedListener(UnityAction<ScreenSide, float> listener)
    {
        freezerEffectActivatedListeners.Add(listener);
        foreach (Pickup invoker in freezerEffectActivatedInvokers)
        {
            invoker.AddFreezerEffectActivatedListener(listener);
        }
    }

    #endregion

    #region Speedup effect activated support

    /// <summary>
    /// Adds the given script as a speedup effect activated invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddSpeedupEffectActivatedInvoker(Pickup invoker)
    {
        speedupEffectActivatedInvokers.Add(invoker);
        foreach (UnityAction<float, float> listener in speedupEffectActivatedListeners)
        {
            invoker.AddSpeedupEffectActivatedListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a speedup effect activated listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddSpeedupEffectActivatedListener(UnityAction<float, float> listener)
    {
        speedupEffectActivatedListeners.Add(listener);
        foreach (Pickup invoker in speedupEffectActivatedInvokers)
        {
            invoker.AddSpeedupEffectActivatedListener(listener);
        }
    }

    #endregion

    #region Player won support

    /// <summary>
    /// Adds the given script as a player won invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddPlayerWonInvoker(HUD invoker)
    {
        playerWonInvokers.Add(invoker);
        foreach (UnityAction<ScreenSide> listener in playerWonListeners)
        {
            invoker.AddPlayerWonListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a player won listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddPlayerWonListener(UnityAction<ScreenSide> listener)
    {
        playerWonListeners.Add(listener);
        foreach (HUD invoker in playerWonInvokers)
        {
            invoker.AddPlayerWonListener(listener);
        }
    }

    #endregion

}
