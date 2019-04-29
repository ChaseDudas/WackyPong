using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPaddle : Paddle
{
    // saved for efficiency
    //Rigidbody2D rb2d;
    //Vector2 newPosition = Vector2.zero;
    //float halfPaddleHeight;
    //float halfPaddleWidth;

    string[] tagsToLookFor =
    { 
        "Ball",
        "Pickup"
    };

    /// <summary>
    /// Fixeds the update.
    /// </summary>
    virtual public void FixedUpdate()
    {
        if(!frozen)
        {
            float input;

            foreach(string tag in tagsToLookFor)
            {

                GameObject[] gos;
                gos = GameObject.FindGameObjectsWithTag(tag);
                Rigidbody2D closest = null;
                float distance = Mathf.Infinity;
                Vector3 position = transform.position;
                if (gos.Length == 0)
                {
                    //Debug.Log("No game objects are tagged with 'Ball' or 'Pickup'");
                    break;
                }
                foreach (GameObject go in gos)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go.GetComponent<Rigidbody2D>();
                        distance = curDistance;
                    }
                }
                //return closest;
                input = closest.transform.position.y;

                if(closest.velocity.x > 0)
                {
                    //ball is moving right

                    if (Mathf.Abs(rb2d.position.y - input)  > ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime)
                    {
                        if (input > rb2d.position.y)
                        {
                            //If ball is above paddle

                            newPosition = rb2d.position;
                            newPosition.y +=
                                ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime;
                            newPosition.y = CalculateClampedY(newPosition.y);
                            rb2d.MovePosition(newPosition);
                        }
                        else if (input < rb2d.position.y)
                        {
                            //If ball is below paddle

                            newPosition = rb2d.position;
                            newPosition.y -=
                                ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime;
                            newPosition.y = CalculateClampedY(newPosition.y);
                            rb2d.MovePosition(newPosition);
                        }
                    }

                }

            }


        }
       
    }

}
