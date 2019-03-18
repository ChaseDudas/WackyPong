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
        spawnRange = ConfigurationUtils.MaxSpawnDelay -
            ConfigurationUtils.MinSpawnDelay;
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GetSpawnDelay();
        spawnTimer.Run();

        spawnTimer.AddTimerListener( SpawnBall );

        EventManager.AddListener( SpawnNewBallEvent );
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //if (spawnTimer.Finished)
        //{
        //    // don't stack with a spawn still pending
        //    retrySpawn = false;
        //    SpawnBall();
        //    spawnTimer.Duration = GetSpawnDelay();
        //    spawnTimer.Run();
        //}

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
        // make sure we don't spawn into a collision
        if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
        {
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
            else 
            {
                Instantiate(prefabFreezerPickup, Vector3.zero, Quaternion.identity);
            }
        }
        else
        {
            retrySpawn = true;
        }
    }

    void SpawnNewBallEvent(ScreenSide useless, int fake)
    {
        SpawnBall();
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
