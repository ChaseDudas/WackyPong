using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A pickup
/// </summary>
public class Pickup : Ball
{
    #region Fields

    // valid for both pickup effects
    float duration;

    // freezer effect
    FreezerEffectActivatedEvent freezerEffectActivatedEvent =
        new FreezerEffectActivatedEvent();

    // speedup effect
    SpeedupEffectActivatedEvent speedupEffectActivatedEvent =
    new SpeedupEffectActivatedEvent();
    #endregion

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public override void Start()
    {
        base.Start();

        // set up freezer effect
        if (ballType == BallType.Freezer)
        {
            duration = ConfigurationUtils.FreezerSeconds;
            EventManager.AddFreezerEffectActivatedInvoker(this);
        }
        if(ballType == BallType.Speedup)
        {
            duration = ConfigurationUtils.SpeedupSeconds;
            EventManager.AddSpeedupEffectActivatedInvoker(this);
        }
    }

    /// <summary>
    /// Check for collision with a paddle
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // check for collision with a paddle
        if (coll.gameObject.CompareTag("LeftPaddle") ||
            coll.gameObject.CompareTag("RightPaddle"))
        {
            // freezer effect processing
            if (ballType == BallType.Freezer)
            {
                if (coll.gameObject.CompareTag("LeftPaddle"))
                {
                    freezerEffectActivatedEvent.Invoke(ScreenSide.Right, duration);
                }
                else if (coll.gameObject.CompareTag("RightPaddle"))
                {
                    freezerEffectActivatedEvent.Invoke(ScreenSide.Left, duration);
                }
            }

            //speedup effect processing
            if(ballType == BallType.Speedup)
            {
                print("Speedup");
                speedupEffectActivatedEvent.Invoke(ScreenSide.Left, duration);
            }

            // invoke event and destroy self
            ballDiedEvent.Invoke();
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Adds the given listener for the freezer effect activated event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddFreezerEffectActivatedListener(UnityAction<ScreenSide, float> listener)
    {
        freezerEffectActivatedEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the pickup effect activated event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddSpeedupEffectActivatedListener(UnityAction<ScreenSide, float> listener)
    {
        speedupEffectActivatedEvent.AddListener(listener);
    }
    #endregion
}
