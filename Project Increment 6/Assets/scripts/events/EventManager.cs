using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    static List<Ball> invokers = new List<Ball> ();
    static List<UnityAction<ScreenSide, int>> listeners = new List<UnityAction<ScreenSide, int>> ();

    /// <summary>
    /// Adds the invoker.
    /// </summary>
    /// <param name="script">Script.</param>
    public static void AddInvoker(Ball invoker)
    {
        invokers.Add(invoker);
        foreach(UnityAction<ScreenSide, int> listener in listeners)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
     }

    /// <summary>
    /// Adds the listener.
    /// </summary>
    /// <param name="handler">Handler.</param>
    public static void AddListener(UnityAction<ScreenSide, int> handler)
    {
        listeners.Add(handler);
        foreach(Ball ball in invokers)
        {
            ball.AddPointsAddedEventListener(handler);
        }
    }


    static List<Paddle> invokersB = new List<Paddle>();
    static List<UnityAction<ScreenSide, int>> listenersB = new List<UnityAction<ScreenSide, int>>();

    /// <summary>
    /// Adds the invoker.
    /// </summary>
    /// <param name="script">Script.</param>
    public static void addInvoker(Paddle invoker)
    {
        invokersB.Add(invoker);
        foreach (UnityAction<ScreenSide, int> listener in listenersB)
        {
            invoker.AddHitPointsAdded(listener);
        }
    }

    /// <summary>
    /// Adds the listener.
    /// </summary>
    /// <param name="handler">Handler.</param>
    public static void addListener(UnityAction<ScreenSide, int> handler)
    {
        listenersB.Add(handler);
        foreach (Paddle paddle in invokersB)
        {
            paddle.AddHitPointsAdded(handler);
        }
    }

    static List<Pickup> invokersP = new List<Pickup>();
    static List<UnityAction<ScreenSide, float>> listenersP = new List<UnityAction<ScreenSide, float>>();

    /// <summary>
    /// Adds the invoker.
    /// </summary>
    /// <param name="script">Script.</param>
    public static void AddInvokerP(Pickup invoker)
    {
        invokersP.Add(invoker);
        foreach (UnityAction<ScreenSide, float> listener in listenersP)
        {
            invoker.AddFreezeListener(listener);
        }
    }

    /// <summary>
    /// Adds the listener.
    /// </summary>
    /// <param name="handler">Handler.</param>
    public static void AddListenerP(UnityAction<ScreenSide, float> handler)
    {
        listenersP.Add(handler);
        foreach (Pickup pickup in invokersP)
        {
            pickup.AddFreezeListener(handler);
        }
    }

}
