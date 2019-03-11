using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball spawner
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBall;

    [SerializeField]
    GameObject bonusBall;

    [SerializeField]
    GameObject freezePickup;

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
        GameObject tempBall = Instantiate<GameObject>(prefabBall);
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
        spawnRange = ConfigurationUtils.MaxSpawnDelay -
            ConfigurationUtils.MinSpawnDelay;
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GetSpawnDelay();
        spawnTimer.Run();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (spawnTimer.Finished)
        {
            // don't stack with a spawn still pending
            retrySpawn = false;
            SpawnBall();
            spawnTimer.Duration = GetSpawnDelay();
            spawnTimer.Run();
        }

        // try again if spawn still pending
        if (retrySpawn)
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
        float rand = Random.Range(0.0f, 1.0f);
        // make sure we don't spawn into a collision
        if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
        {
            retrySpawn = false;
            if(rand < ConfigurationUtils.StandardBallSpawnPercentage)
            {
                Instantiate(prefabBall, Vector3.zero, Quaternion.identity);
            }
            else if(rand < ConfigurationUtils.StandardBallSpawnPercentage + ConfigurationUtils.BonusBallSpawnPercentage)
            {
                Instantiate(bonusBall, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Instantiate(freezePickup, Vector3.zero, Quaternion.identity);
            }
        }
        else
        {
            retrySpawn = true;
        }
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Gets the spawn delay in seconds for the next ball spawn
    /// </summary>
    /// <returns>spawn delay</returns>
    float GetSpawnDelay()
    {
        return ConfigurationUtils.MinSpawnDelay +
            Random.value * spawnRange;
    }

    #endregion
}
