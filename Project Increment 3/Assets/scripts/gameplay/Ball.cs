using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    // move delay timer
    Timer moveTimer;

    // ball death support
    Timer deathTimer;

    // save for efficiency
    Rigidbody2D rb2d;

    float hits;
    float points;

    ScreenSide side;

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // start move timer
        moveTimer = gameObject.AddComponent<Timer>();
        moveTimer.Duration = 1;
        moveTimer.Run();

        // start death timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = ConfigurationUtils.BallLifeSeconds;
        deathTimer.Run();

        // save for efficiency
        rb2d = GetComponent<Rigidbody2D>();

        hits = ConfigurationUtils.BallHits;
        points = ConfigurationUtils.ScorePoints;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // start moving as appropriate
        if (moveTimer.Finished)
        {
            moveTimer.Stop();
            StartMoving();
        }

        // destroy ball on death
        if (deathTimer.Finished)
        {
            // spawn a new ball and destroy self
            Camera.main.GetComponent<BallSpawner>().SpawnBall();
            Destroy(gameObject);
        }
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
                // spawn a new ball and destroy self
                Camera.main.GetComponent<BallSpawner>().SpawnBall();
                bool exitedLeft = Camera.main.transform.position.x > transform.position.x;
                if(exitedLeft)
                {
                    GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddScore(ScreenSide.Right, points);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddScore(ScreenSide.Left, points);
                }
                Destroy(gameObject);
            }
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

    public float Hits
    {
        get { return hits; }
    }

    public float Points
    {
        get { return points; }
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

    #endregion
}
