using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 speed;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        var speed = ConfigurationUtils.BallImpulseForce;

        rb.velocity = new Vector2(1 * Random.Range(0, 2) * 2 - 1 , Random.Range(-1.0f,1.0f)) * speed;
    }

    /// <summary>
    ///     Change the direction of the ball
    /// </summary>
    public void SetDirection(Vector2 dir)
    {
        rb.velocity = dir * rb.velocity.magnitude *
            // The line below makes it more like real pong.
            (Mathf.Max(Mathf.Abs(dir.y), 0.5f) * 2.1f);
    }

    /// <summary>
    ///     Destory ball when it leaves the field
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
