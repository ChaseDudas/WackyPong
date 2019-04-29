using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameUtils
{
    static ConfigurationData configurationData;

    static float ballImpulseForce;
    static float minSpawnDelay;
    static float maxSpawnDelay;
    static GameType gameType;


    public static void HandleGameStartedEvent(GameType gt, DifficultyType dt)
    {

        switch (gt)
        {
            case GameType.OnePlayer:
                gameType = GameType.OnePlayer;
                break;
            case GameType.TwoPlayer:
                gameType = GameType.TwoPlayer;
                break;
        }
        switch (dt)
        {
            case DifficultyType.Easy:
                ballImpulseForce = configurationData.EasyBallImpulseForce;
                minSpawnDelay = configurationData.EasyMinSpawnDelay;
                maxSpawnDelay = configurationData.EasyMaxSpawnDelay;
                break;

            case DifficultyType.Medium:
                ballImpulseForce = configurationData.MediumBallImpulseForce;
                minSpawnDelay = configurationData.MediumMinSpawnDelay;
                maxSpawnDelay = configurationData.MediumMaxSpawnDelay;
                break;

            case DifficultyType.Hard:
                ballImpulseForce = configurationData.HardBallImpulseForce;
                minSpawnDelay = configurationData.HardMinSpawnDelay;
                maxSpawnDelay = configurationData.HardMaxSpawnDelay;
                break;
        }
    }

    public static GameType GetGameType
    {
        get { return gameType; }
    }

    /// <summary>
    /// Gets the impulse force to apply to a ball
    /// to get it moving
    /// </summary>
    public static float BallImpulseForce
    {
        get { return ballImpulseForce; }
    }


    /// <summary>
    /// Gets the min spawn delay for ball spawning
    /// </summary>
    public static float MinSpawnDelay
    {
        get { return minSpawnDelay; }
    }

    /// <summary>
    /// Gets the max spawn delay for ball spawning
    /// </summary>
    public static float MaxSpawnDelay
    {
        get { return maxSpawnDelay; }
    }


    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}

