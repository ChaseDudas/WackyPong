using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A paddle
/// </summary>
public class Paddle : MonoBehaviour
{
    #region Fields

    [SerializeField]
    ScreenSide side;

    // saved for efficiency
    Rigidbody2D rb2d;
    Vector2 newPosition = Vector2.zero;
    float halfPaddleHeight;
    float halfPaddleWidth;

    // aiming support
    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    // freeze support
    bool frozen = false;
    Timer freezeTimer;

    // events invoked by class
    HitsAddedEvent hitsAddedEvent = new HitsAddedEvent();

    #endregion

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // saved for efficiency
        rb2d = GetComponent<Rigidbody2D>();
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
        halfPaddleHeight = bc2d.size.y / 2;
        halfPaddleWidth = bc2d.size.x / 2;

        // freeze support
        freezeTimer = gameObject.AddComponent<Timer>();
        freezeTimer.AddTimerFinishedListener(Unfreeze);
        EventManager.AddFreezerEffectActivatedListener(Freeze);

        // add as hits added invoker
        EventManager.AddHitsAddedInvoker(this);
    }

    /// <summary>
    /// FixedUpdate is called 50 times a second
    /// </summary>
    void FixedUpdate()
    {
        if (!frozen)
        {
            // get side-specific input
            float input;
            if (side == ScreenSide.Left)
            {
                input = Input.GetAxis("LeftPaddle");
            }
            else
            {
                input = Input.GetAxis("RightPaddle");
            }

            // move paddle as appropriate
            if (input != 0)
            {
                newPosition = rb2d.position;
                newPosition.y += input *
                    ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime;
                newPosition.y = CalculateClampedY(newPosition.y);
                rb2d.MovePosition(newPosition);
            }
        }
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball") &&
            FrontCollision(coll))
        {
            // add hits for paddle
            hitsAddedEvent.Invoke(side, 
                coll.gameObject.GetComponent<Ball>().Hits);

            // calculate new ball direction
            float ballOffsetFromPaddleCenter =
                coll.transform.position.y - transform.position.y;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfPaddleHeight;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;

            // angle modification is based on screen side
            float angle;
            if (side == ScreenSide.Left)
            {
                angle = angleOffset;
            }
            else
            {
                angle = (float)(Mathf.PI - angleOffset);
            }
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Adds the given listener for the hits added event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddHitsAddedListener(UnityAction<ScreenSide, int> listener)
    {
        hitsAddedEvent.AddListener(listener);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Calculates a y position to clamp the paddle in the screen
    /// </summary>
    /// <param name="y">the y position to clamp</param>
    /// <returns>the clamped y position</returns>
    float CalculateClampedY(float y)
    {
        // clamp top and bottom edges
        if (y + halfPaddleHeight > ScreenUtils.ScreenTop)
        {
            y = ScreenUtils.ScreenTop - halfPaddleHeight;
        }
        else if (y - halfPaddleHeight < ScreenUtils.ScreenBottom)
        {
            y = ScreenUtils.ScreenBottom + halfPaddleHeight;
        }
        return y;
    }

    /// <summary>
    /// Checks for a collision at the front of the paddle
    /// </summary>
    /// <returns><c>true</c>, if collision was at the front of the paddle, <c>false</c> otherwise.</returns>
    /// <param name="coll">collision info</param>
    bool FrontCollision(Collision2D coll)
    {
        const float tolerance = 0.05f;

        // on front collisions, both contact points are at the same x location
        ContactPoint2D[] contacts = coll.contacts;
        return Mathf.Abs(contacts[0].point.x - contacts[1].point.x) < tolerance;
    }

    /// <summary>
    /// Freezes the paddle for the given duration
    /// </summary>
    /// <param name="freezeSide">side to freeze</param>
    /// <param name="duration">duration</param>
    void Freeze(ScreenSide freezeSide, float duration)
    {
        if (freezeSide == side)
        {
            // freeze paddle and run or add time to timer
            frozen = true;
            if (!freezeTimer.Running)
            {
                freezeTimer.Duration = duration;
                freezeTimer.Run();
            }
            else
            {
                freezeTimer.AddTime(duration);
            }
        }
    }

    /// <summary>
    /// Unfreezes the paddle
    /// </summary>
    void Unfreeze()
    {
        frozen = false;
        freezeTimer.Stop();
    }

    #endregion
}
