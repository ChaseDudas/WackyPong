using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    float paddleMoveUnitsPerSecond = 10;
    float ballImpulseForce = 5;
    float ballLifeSeconds = 10;
    float minSpawnDelay = 5;
    float maxSpawnDelay = 10;
    int standardPoints = 1;
    int standardHits = 1;
    int bonusPoints = 2;
    int bonusHits = 2;
    float freezerSeconds = 2;
    float speedupSeconds = 2;
    float speedupFactor = 2;
    float standardBallSpawnProbability = 0.6f;
    float bonusBallSpawnProbability = 0.2f;
    float freezerPickupSpawnProbability = 0.1f;
    float speedupPickupSpawnProbability = 0.1f;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }

    public float BallLifeSeconds
    {
        get { return ballLifeSeconds; }
    }

    public float MinSpawnDelay
    {
        get { return minSpawnDelay; }
    }

    public float MaxSpawnDelay
    {
        get { return maxSpawnDelay; }
    }

    public int StandardPoints
    {
        get { return standardPoints; }
    }

    public int StandardHits
    {
        get { return standardHits; }
    }

    public int BonusPoints
    {
        get { return bonusPoints; }
    }

    public int BonusHits
    {
        get { return bonusHits; }
    }

    public float FreezerSeconds
    {
        get { return freezerSeconds; }
    }

    public float SpeedupSeconds
    {
        get { return speedupSeconds; }
    }

    public float SpeedupFactor
    {
        get { return speedupFactor; }
    }

    public float StandardBallSpawnProbability
    {
        get { return standardBallSpawnProbability; }
    }

    public float BonusBallSpawnProbability
    {
        get { return bonusBallSpawnProbability; }
    }

    public float FreezerPickupSpawnProbability
    {
        get { return freezerPickupSpawnProbability; }
    }

    public float SpeedupPickupSpawnProbability
    {
        get { return speedupPickupSpawnProbability; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader input = null;
        try
        {
            //create stream reader object
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));

            //read in names and values
            string names = input.ReadLine();
            string values = input.ReadLine();

            SetConfigurationDataFields(values);
        }
        catch(Exception e)
        {
        }
        finally
        {
            //Close the input file
            if(input != null)
            {
                input.Close();
            }
        }
    }

    #endregion

    void SetConfigurationDataFields(string csvValues)
    {
        //Know the order in which data is recieved
        string[] values = csvValues.Split(',');

        // configuration data
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        ballImpulseForce = float.Parse(values[1]);
        ballLifeSeconds = float.Parse(values[2]);
        minSpawnDelay = float.Parse(values[3]);
        maxSpawnDelay = float.Parse(values[4]);
        standardPoints = int.Parse(values[5]);
        standardHits = int.Parse(values[6]);
        bonusPoints = int.Parse(values[7]);
        bonusHits = int.Parse(values[8]);
        freezerSeconds = float.Parse(values[9]);
        speedupSeconds = float.Parse(values[10]);
        speedupFactor = float.Parse(values[11]);
        standardBallSpawnProbability = float.Parse(values[12]);
        bonusBallSpawnProbability = float.Parse(values[13]);
        freezerPickupSpawnProbability = float.Parse(values[14]);
        speedupPickupSpawnProbability = float.Parse(values[15]);
    }
}
