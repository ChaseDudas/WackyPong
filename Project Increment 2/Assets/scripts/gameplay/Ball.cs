using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    Timer lifetimer;
    float startTime;
    Timer delayTimer;

    // save for efficiency
    Rigidbody2D rb2d;
    Vector2 force;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        // add life timer
        lifetimer = gameObject.AddComponent<Timer>();
        lifetimer.Duration = ConfigurationUtils.BallLifetime;
        lifetimer.Run();

        // add a ball delay timer
        delayTimer = gameObject.AddComponent<Timer>();
        delayTimer.Duration = 3;
        delayTimer.Run();

        startTime = Time.time;

        // save for efficiency
        rb2d = GetComponent<Rigidbody2D>();

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
        force = new Vector2(
            (float)Mathf.Cos(angle) * ConfigurationUtils.BallImpulseForce,
            (float)Mathf.Sin(angle) * ConfigurationUtils.BallImpulseForce);

        //moved to update to account for delay timer
        //rb2d.AddForce(force, ForceMode2D.Impulse);
       
    }
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        //If life timer has ended kill ball
        if (lifetimer.Finished)
        {
            destroyMakeBall();
        }

        //Delay timer to make the game fair
        if(delayTimer.Stop())
        {
            rb2d.AddForce(force, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Destroys ball when it becomes invisible
    /// </summary>
    void OnBecameInvisible()
    {
        //don’t spawn a ball in that method if the ball became invisible because the death timer finished. 
        if(!lifetimer.Finished)
        {
            destroyMakeBall();
        }
    }

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
    /// Create a destroy ball objects
    /// </summary>
    public void destroyMakeBall()
    {
        Destroy(gameObject);

        var cam = Camera.main;
        var ballSpawner = cam.GetComponent<BallSpawner>();
        ballSpawner.SpawnBall();

    }
}
