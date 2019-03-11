using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball spawner
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab;

    // ball death support
    Timer spawnTimer;

    //Bool to indicate whether our spawn location is collision free or not
    bool spawnBall = false;
    bool pendingSpawn = false;

    float ballSpawnHalfWidth;
    float ballSpawnHalfHeight;
    static Vector2 topLeft = new Vector2();
    static Vector2 botRight = new Vector2();

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {

        //Get bounds
        GameObject temp = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        BoxCollider2D collider = temp.GetComponent<BoxCollider2D>();
        ballSpawnHalfWidth = collider.size.x / 2;
        ballSpawnHalfHeight = collider.size.y / 2;
        Destroy(temp);

        topLeft.x = -ballSpawnHalfWidth;
        topLeft.y = -ballSpawnHalfHeight;
        botRight.x = ballSpawnHalfWidth;
        botRight.y = ballSpawnHalfHeight;

        // start death timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(ConfigurationUtils.MinSpawnDelay, ConfigurationUtils.MaxSpawnDelay);
        spawnTimer.Run();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //Check if we can spawn a ball
        CheckCleanSpawn();

        if (spawnTimer.Finished)
        {
            spawnTimer.Stop();
            SpawnBall();
        }
        if(pendingSpawn)
        {
            SpawnBall();
        }
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Spawns a ball in the center of the screen
    /// </summary>
    public void SpawnBall()
    {
        if(spawnBall)
        {
            Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
            pendingSpawn = false;
        }
        else
        {
            pendingSpawn = true;
        }
    }

    public void CheckCleanSpawn()
    {
        if (Physics2D.OverlapArea(topLeft, botRight) != null)
        {
            //There is a collision
            spawnBall = false;
        }
        else 
        { 
            //No collision
            spawnBall = true; 
        }
    }

    #endregion
}
