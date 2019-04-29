using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball spawner
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabStandardBall;
    [SerializeField]
    GameObject prefabBonusBall;
    [SerializeField]
    GameObject prefabFreezerPickup;
    [SerializeField]
    GameObject prefabSpeedupPickup;

    // spawn support
    Timer spawnTimer;
    float spawnRange;

    // collision-free support
    bool retrySpawn = false;
    Vector2 spawnLocationMin;
    Vector2 spawnLocationMax;

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // spawn and destroy ball to calculate
        // spawn location min and max
        GameObject tempBall = Instantiate<GameObject>(prefabStandardBall);
        BoxCollider2D collider = tempBall.GetComponent<BoxCollider2D>();
        float ballColliderHalfWidth = collider.size.x / 2;
        float ballColliderHalfHeight = collider.size.y / 2;
        Vector2 spawnLocation = Vector2.zero;
        spawnLocationMin = new Vector2(
            spawnLocation.x - ballColliderHalfWidth,
            spawnLocation.y - ballColliderHalfHeight);
        spawnLocationMax = new Vector2(
            spawnLocation.x + ballColliderHalfWidth,
            spawnLocation.y + ballColliderHalfHeight);
        Destroy(tempBall);

        // initialize and start spawn timer
        spawnRange = GameUtils.MaxSpawnDelay -
            GameUtils.MinSpawnDelay;
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GetSpawnDelay();
        spawnTimer.Run();
        spawnTimer.AddTimerFinishedListener(HandleSpawnTimerFinished);

        // add as listener for events
        EventManager.AddBallLostListener(HandleBallLostEvent);
        EventManager.AddBallDiedListener(HandleBallDiedEvent);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // try again if spawn still pending
        if (retrySpawn)
        {
            SpawnBall();
        }
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Spawns a ball in the center of the screen
    /// </summary>
    void SpawnBall()
    {
        // make sure we don't spawn into a collision
        if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
        {
            AudioManager.Play(AudioClipName.BallSpawn);
            retrySpawn = false;

            // pick random ball type to spawn
            float randomType = Random.value;
            if (randomType < ConfigurationUtils.StandardBallSpawnProbability)
            {
                Instantiate(prefabStandardBall, Vector3.zero, Quaternion.identity);
            }
            else if (randomType < ConfigurationUtils.StandardBallSpawnProbability +
                ConfigurationUtils.BonusBallSpawnProbability)
            {
                Instantiate(prefabBonusBall, Vector3.zero, Quaternion.identity);
            }
            else if (randomType < ConfigurationUtils.StandardBallSpawnProbability +
                ConfigurationUtils.BonusBallSpawnProbability +
                ConfigurationUtils.FreezerPickupSpawnProbability)

            {
                Instantiate(prefabFreezerPickup, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Instantiate(prefabSpeedupPickup, Vector3.zero, Quaternion.identity);
            }
        }
        else
        {
            retrySpawn = true;
        }
    }

    /// <summary>
    /// Gets the spawn delay in seconds for the next ball spawn
    /// </summary>
    /// <returns>spawn delay</returns>
    float GetSpawnDelay()
    {
        return GameUtils.MinSpawnDelay +
            Random.value * spawnRange;
    }

    /// <summary>
    /// Handles the spawn timer finishing
    /// </summary>
    void HandleSpawnTimerFinished()
    {
        // don't stack with a spawn still pending
        retrySpawn = false;
        SpawnBall();
        spawnTimer.Duration = GetSpawnDelay();
        spawnTimer.Run();
    }

    /// <summary>
    /// Handles the ball lost event by spawning a ball
    /// </summary>
    /// <param name="unusedSide">unused</param>
    /// <param name="unusedInt">unused</param>
    void HandleBallLostEvent(ScreenSide unusedSide, int unusedInt)
    {
        SpawnBall();
    }

    /// <summary>
    /// Handles the ball died event by spawning a ball
    /// </summary>
    void HandleBallDiedEvent()
    {
        SpawnBall();
    }

    #endregion
}
