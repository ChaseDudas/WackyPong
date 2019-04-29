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
    float speedupFactor;
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

        // set up effects
        if (ballType == BallType.Freezer)
        {
            duration = ConfigurationUtils.FreezerSeconds;
            EventManager.AddFreezerEffectActivatedInvoker(this);
        }
        else if (ballType == BallType.Speedup)
        {
            duration = ConfigurationUtils.SpeedupSeconds;
            speedupFactor = ConfigurationUtils.SpeedupFactor;
            EventManager.AddSpeedupEffectActivatedInvoker(this);
        }
    }

    /// <summary>
    /// Check for collision with a paddle
    /// </summary>
    /// <param name="coll">collision info</param>
    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        // check for collision with a paddles
        if (coll.gameObject.CompareTag("LeftPaddle") ||
            coll.gameObject.CompareTag("RightPaddle"))
        {
            AudioManager.Play(AudioClipName.BallCollision);

            // effect processing
            if (ballType == BallType.Freezer)
            {
                AudioManager.Play(AudioClipName.FreezerEffectActivated);
                if (coll.gameObject.CompareTag("LeftPaddle"))
                {
                    freezerEffectActivatedEvent.Invoke(ScreenSide.Right, duration);
                }
                else if (coll.gameObject.CompareTag("RightPaddle"))
                {
                    freezerEffectActivatedEvent.Invoke(ScreenSide.Left, duration);
                }
            }
            else if (ballType == BallType.Speedup)
            {
                AudioManager.Play(AudioClipName.SpeedupEffectActivated);
                speedupEffectActivatedEvent.Invoke(speedupFactor, duration);
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
    /// Adds the given listener for the speedup effect activated event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddSpeedupEffectActivatedListener(UnityAction<float, float> listener)
    {
        speedupEffectActivatedEvent.AddListener(listener);
    }

    #endregion
}
