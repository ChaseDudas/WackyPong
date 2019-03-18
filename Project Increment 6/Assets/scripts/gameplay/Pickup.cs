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

    protected FreezePaddleEvent freezePaddleEvent = new FreezePaddleEvent();
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
        }

        //Add invoker
        EventManager.AddInvokerP(this);
    }

    /// <summary>
    /// Check for collision with a paddle
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // freezer effect processing
        if (ballType == BallType.Freezer)
        {
            if (coll.gameObject.CompareTag("LeftPaddle"))
            {
                //GameObject paddle = GameObject.FindGameObjectWithTag("RightPaddle");
                //Paddle paddleScript = paddle.GetComponent<Paddle>();
                //paddleScript.Freeze(ScreenSide.Right, duration);
                InvokeFreezeEffect(ScreenSide.Right);
            }
            else if (coll.gameObject.CompareTag("RightPaddle"))
            {
                //GameObject paddle = GameObject.FindGameObjectWithTag("LeftPaddle");
                //Paddle paddleScript = paddle.GetComponent<Paddle>();
                //paddleScript.Freeze(ScreenSide.Left, duration);
                InvokeFreezeEffect(ScreenSide.Left);
            }
        }

        // spawn a new ball and destroy self
        Camera.main.GetComponent<BallSpawner>().SpawnBall();
        Destroy(gameObject);
    }

    public void InvokeFreezeEffect(ScreenSide side)
    {
        switch (base.GetBallType())
        {
            case BallType.Freezer:
                freezePaddleEvent.Invoke(side, duration);
                break;
        }
    }

    public void AddFreezeListener(UnityAction<ScreenSide, float> listener)
    {
        freezePaddleEvent.AddListener(listener);
    }
    #endregion
}
