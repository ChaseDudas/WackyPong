using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup
    :
    Ball
{
    int freezeDuration;
    Timer freezeTimer;
    ScreenSide side;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (ballType == BallType.Freezer)
        {
            freezeDuration = ConfigurationUtils.FreezerEffectDuration;
        }
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        var leftPaddle = GameObject.FindGameObjectWithTag("LeftPaddle").GetComponent<Paddle>();
        var rightPaddle = GameObject.FindGameObjectWithTag("RightPaddle").GetComponent<Paddle>();

        if (coll.gameObject.name == "RightPaddle")
        {
            leftPaddle.FreezePaddleForDuration(freezeDuration);
        }
        else
        {
            rightPaddle.FreezePaddleForDuration(freezeDuration);
        }

        // spawn a new ball and destroy self
        Destroy(gameObject);
        Camera.main.GetComponent<BallSpawner>().SpawnBall();

    }
}

