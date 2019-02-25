using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return 10; }
    }

    /// <summary>
    /// Gets the impulse force to apply to a ball
    /// to get it moving
    /// </summary>
    public static float BallImpulseForce
    {
        get { return 5; }
    }

    /// <summary>
    /// Gets how many seconds a ball stays alive
    /// </summary>
    public static float BallLifeSeconds
    {
        get { return 10; }
    }

    /// <summary>
    ///     Standard ball score
    /// </summary>
    public static float BallHits
    {
        get { return 1; }
    }

    public static float ScorePoints
    {
        get { return 1; }
    }

    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {

    }
}
