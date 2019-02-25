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

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    #endregion

    #region Public methods

    /// <summary>
    /// Spawns a ball in the center of the screen
    /// </summary>
    public void SpawnBall()
    {
        Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
    }

    #endregion
}
