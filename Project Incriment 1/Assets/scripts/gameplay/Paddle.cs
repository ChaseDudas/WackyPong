using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A paddle
/// </summary>
public class Paddle : MonoBehaviour
{

    //VARIABLE DECLERATIONS
    Rigidbody2D rb;
    [SerializeField] ScreenSide MySide;
    Vector2 moveMe;
    float speed;
    float halfHeight;
    float halfWidth;
    const float bounceAngleHalfRange = 60.0f * Mathf.Deg2Rad;


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        var pos = transform.position;
        moveMe = new Vector3(pos.x, pos.y, pos.z);

        speed = ConfigurationUtils.PaddleMoveUnitsPerSecond;

        var box = GetComponent<BoxCollider2D>();
        halfHeight = box.size.y / 2.0f;
        halfWidth = box.size.x / 2.0f;
    }

    /// <summary>
    ///     Paddle movement
    /// </summary>
    private void FixedUpdate()
    {
        float rInput = Input.GetAxis("RightPaddle");
        float lInput = Input.GetAxis("LeftPaddle");

        //Move paddles
        switch (MySide)
        {
            case ScreenSide.Left:
                if (lInput != 0.0f)
                {
                    moveMe.y += lInput * speed * Time.deltaTime;
                }
                break;

            case ScreenSide.Right:
                if (rInput != 0.0f)
                {
                    moveMe.y += rInput * speed * Time.deltaTime;
                }
                break;
            default:
                print("You will never get this!");
                break;
        }

        moveMe.y = FindClampedY(moveMe.y);
        rb.MovePosition(moveMe);
    }

    /// <summary>
    ///     Bounds paddles to the play field
    /// </summary>
    /// <returns>a float</returns>
    float FindClampedY(float possibleNewY)
    {
        // Set up initial vars.
        var sTop = ScreenUtils.ScreenTop;
        var sBot = ScreenUtils.ScreenBottom;
        float newPos = 0.0f;

        // Check for hitting top, bot, or neither.
        if (possibleNewY + halfHeight > sTop)
        { // Hitting your head on the roof.
            newPos = sTop - halfHeight;
        }
        else if (possibleNewY - halfHeight < sBot)
        { // If you land on the floor.
            newPos = sBot + halfHeight;
        }
        else
        { // You hit nothing so u stay in the same place.
            newPos = possibleNewY;
        }

        // Give back your new y position.
        return (newPos);
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter =
                coll.transform.position.y - transform.position.y;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfHeight;
            float angleOffset = normalizedBallOffset * bounceAngleHalfRange;

            // angle modification is based on screen side
            float angle;
            if (MySide == ScreenSide.Left)
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

}
