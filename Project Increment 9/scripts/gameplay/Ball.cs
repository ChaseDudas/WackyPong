using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    #region Fields

    [SerializeField]
    protected BallType ballType;

    // points and hits the ball is worth
    int points;
    int hits;

    // move delay timer
    Timer moveTimer;

    // ball death support
    Timer deathTimer;

    // save for efficiency
    Rigidbody2D rb2d;

    // speedup support
    float currentSpeedupFactor;
    Timer speedupTimer;

    // events invoked by the class
    BallLostEvent ballLostEvent = new BallLostEvent();
    protected BallDiedEvent ballDiedEvent = new BallDiedEvent();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of hits the ball is worth
    /// </summary>
    public int Hits
    {
        get { return hits; }
    }

    #endregion

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public virtual void Start()
    {
        // set number of points and hits the ball is worth
        if (ballType == BallType.Standard)
        {
            points = ConfigurationUtils.StandardPoints;
            hits = ConfigurationUtils.StandardHits;
        }
        else if (ballType == BallType.Bonus)
        {
            points = ConfigurationUtils.BonusPoints;
            hits = ConfigurationUtils.BonusHits;
        }

        // start move timer
        moveTimer = gameObject.AddComponent<Timer>();
        moveTimer.Duration = 1;
        moveTimer.Run();
        moveTimer.AddTimerFinishedListener(HandleMoveTimerFinished);

        // start death timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = ConfigurationUtils.BallLifeSeconds;
        deathTimer.Run();
        deathTimer.AddTimerFinishedListener(HandleDeathTimerFinished);

        // save for efficiency
        rb2d = GetComponent<Rigidbody2D>();

        // speedup support
        speedupTimer = gameObject.AddComponent<Timer>();
        speedupTimer.AddTimerFinishedListener(Slowdown);
        EventManager.AddSpeedupEffectActivatedListener(Speedup);

        // add as invoker of various events
        EventManager.AddBallLostInvoker(this);
        EventManager.AddBallDiedInvoker(this);
    }

    /// <summary>
    /// Destroys ball when it becomes invisible
    /// </summary>
    void OnBecameInvisible()
    {
        // death timer destruction is in Update
        if (!deathTimer.Finished)
        {
            // only lost ball if outside screen
            if (OutsideScreen())
            {
                AudioManager.Play(AudioClipName.BallLost);

                // invoke ball lost event
                if (transform.position.x > 0)
                {
                    ballLostEvent.Invoke(ScreenSide.Left, points);
                }
                else
                {
                    ballLostEvent.Invoke(ScreenSide.Right, points);
                }

                // destroy self
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Play sound on collision with other balls
    /// </summary>
    /// <param name="coll">collision info</param>
    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioClipName.BallCollision);
        }
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Sets the ball direction to the given direction
    /// </summary>
    /// <param name="direction">direction</param>
    public void SetDirection(Vector2 direction)
    {
        // get current rigidbody speed
        float speed = rb2d.velocity.magnitude;
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Adds the given listener for the points added event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddBallLostListener(UnityAction<ScreenSide, int> listener)
    {
        ballLostEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the ball died event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddBallDiedListener(UnityAction listener)
    {
        ballDiedEvent.AddListener(listener);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Starts the ball moving
    /// </summary>
    void StartMoving()
    {
        // set min and max angles going to the right
        float minAngle = -45 * Mathf.Deg2Rad;
        float maxAngle = 45 * Mathf.Deg2Rad;

        // switch to going to the left half the time
        if (Random.value < 0.5)
        {
            minAngle += Mathf.PI;
            maxAngle += Mathf.PI;
        }

        // build and apply force vector
        float angle = Random.Range(minAngle, maxAngle);
        Vector2 force = new Vector2(
            (float)Mathf.Cos(angle) * ConfigurationUtils.BallImpulseForce,
            (float)Mathf.Sin(angle) * ConfigurationUtils.BallImpulseForce);

        // adjust as necessary if speedup effect is active
        if (EffectUtils.SpeedupEffectActive)
        {
            Speedup(EffectUtils.SpeedupFactor,
                EffectUtils.SpeedupEffectSecondsLeft);
            force *= currentSpeedupFactor;
        }

        rb2d.AddForce(force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Tells whether or not the ball it outside the screen horizontally
    /// </summary>
    /// <returns>true if ball outside screen horizontally, false otherwise</returns>
    bool OutsideScreen()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        float halfBallWidth = collider.size.x / 2;
        return (transform.position.x + halfBallWidth < ScreenUtils.ScreenLeft) ||
            (transform.position.x - halfBallWidth > ScreenUtils.ScreenRight);
    }

    /// <summary>
    /// Starts the ball moving when the move timer finishes
    /// </summary>
    void HandleMoveTimerFinished()
    {
        moveTimer.Stop();
        StartMoving();
    }

    /// <summary>
    /// Kills ball when the death timer finishes
    /// </summary>
    void HandleDeathTimerFinished()
    {
        // invoke event and destroy self
        ballDiedEvent.Invoke();
        Destroy(gameObject);
    }

    /// <summary>
    /// Speeds the ball up by the given factor for the given duration
    /// </summary>
    /// <param name="speedupFactor">speedup factor</param>
    /// <param name="duration">duration</param>
    public void Speedup(float speedupFactor, float duration)
    {
        // speedup ball and run or add time to timer
        this.currentSpeedupFactor = speedupFactor;
        rb2d.velocity *= speedupFactor;
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
    /// Slows the ball down to its original speed
    /// </summary>
    void Slowdown()
    {
        rb2d.velocity /= currentSpeedupFactor;
        speedupTimer.Stop();
    }

    #endregion
}
