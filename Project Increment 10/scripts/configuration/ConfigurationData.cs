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
    static float paddleMoveUnitsPerSecond = 10;
    //static float ballImpulseForce = 5;
    static float ballLifeSeconds = 10;
    //static float minSpawnDelay = 5;
    //static float maxSpawnDelay = 10;
    static int standardPoints = 1;
    static int standardHits = 1;
    static int bonusPoints = 2;
    static int bonusHits = 2;
    static float freezerSeconds = 2;
    static float speedupSeconds = 2;
    static float speedupFactor = 2;
    static float standardBallSpawnProbability = 0.6f;
    static float bonusBallSpawnProbability = 0.2f;
    static float freezerPickupSpawnProbability = 0.1f;
    static float speedupPickupSpawnProbability = 0.1f;
    static float easyBallImpulseForce = 5;
    static float mediumBallImpulseForce = 10;
    static float hardBallImpulseForce = 15;
    static float easyMinSpawnDelay = 5;
    static float easyMaxSpawnDelay = 10;
    static float mediumMinSpawnDelay = 3;
    static float mediumMaxSpawnDelay = 8;
    static float hardMinSpawnDelay = 1;
    static float hardMaxSpawnDelay = 5;

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
    public float EasyBallImpulseForce
    {
        get { return easyBallImpulseForce; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float MediumBallImpulseForce
    {
        get { return mediumBallImpulseForce; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float HardBallImpulseForce
    {
        get { return hardBallImpulseForce; }
    }

    /// <summary>
    /// Gets how many seconds a ball stays alive
    /// </summary>
    public float BallLifeSeconds
    {
        get { return ballLifeSeconds; }
    }

    /// <summary>
    /// Gets the min spawn delay for ball spawning
    /// </summary>
    public float EasyMinSpawnDelay
    {
        get { return easyMinSpawnDelay; }
    }

    /// <summary>
    /// Gets the max spawn delay for ball spawning
    /// </summary>
    public float EasyMaxSpawnDelay
    {
        get { return easyMaxSpawnDelay; }
    }

    /// <summary>
    /// Gets the min spawn delay for ball spawning
    /// </summary>
    public float MediumMinSpawnDelay
    {
        get { return mediumMinSpawnDelay; }
    }

    /// <summary>
    /// Gets the max spawn delay for ball spawning
    /// </summary>
    public float MediumMaxSpawnDelay
    {
        get { return mediumMaxSpawnDelay; }
    }

    /// <summary>
    /// Gets the min spawn delay for ball spawning
    /// </summary>
    public float HardMinSpawnDelay
    {
        get { return hardMinSpawnDelay; }
    }

    /// <summary>
    /// Gets the max spawn delay for ball spawning
    /// </summary>
    public float HardMaxSpawnDelay
    {
        get { return hardMaxSpawnDelay; }
    }

    /// <summary>
    /// Gets the number of points a standard ball is worth
    /// </summary>
    public int StandardPoints
    {
        get { return standardPoints; }
    }

    /// <summary>
    /// Gets the number of hits a standard ball is worth
    /// </summary>
    public int StandardHits
    {
        get { return standardHits; }
    }

    /// <summary>
    /// Gets the number of points a bonus ball is worth
    /// </summary>
    public int BonusPoints
    {
        get { return bonusPoints; }
    }

    /// <summary>
    /// Gets the number of hits a bonus ball is worth
    /// </summary>
    public int BonusHits
    {
        get { return bonusHits; }
    }

    /// <summary>
    /// Gets the number of seconds the freezer effect lasts
    /// </summary>
    public float FreezerSeconds
    {
        get { return freezerSeconds; }
    }

    /// <summary>
    /// Gets the number of seconds the speedup effect lasts
    /// </summary>
    public float SpeedupSeconds
    {
        get { return speedupSeconds; }
    }

    /// <summary>
    /// Gets the speedup multiplier
    /// </summary>
    public float SpeedupFactor
    {
        get { return speedupFactor; }
    }

    /// <summary>
    /// Gets the probability of spawning a standard ball
    /// </summary>
    public float StandardBallSpawnProbability
    {
        get { return standardBallSpawnProbability; }
    }

    /// <summary>
    /// Gets the probability of spawning a bonus ball
    /// </summary>
    public float BonusBallSpawnProbability
    {
        get { return bonusBallSpawnProbability; }
    }

    /// <summary>
    /// Gets the probability of spawning a freezer pickup
    /// </summary>
    public float FreezerPickupSpawnProbability
    {
        get { return freezerPickupSpawnProbability; }
    }

    /// <summary>
    /// Gets the probability of spawning a speedup pickup
    /// </summary>
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
        // read and save configuration data from file
        StreamReader input = null;
        try
        {
            // create stream reader object
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));

            // read in names and values
            string names = input.ReadLine();
            string values = input.ReadLine();

            // set configuration data fields
            SetConfigurationDataFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            // always close input file
            if (input != null)
            {
                input.Close();
            }
        }
    }

    #endregion

    /// <summary>
    /// Sets the configuration data fields from the provided
    /// csv string
    /// </summary>
    /// <param name="csvValues">csv string of values</param>
    void SetConfigurationDataFields(string csvValues)
    {
        // the code below assumes we know the order in which the
        // values appear in the string. We could do something more
        // complicated with the names and values, but that's not
        // necessary here
        string[] values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        //ballImpulseForce = float.Parse(values[1]);
        ballLifeSeconds = float.Parse(values[1]);
        //minSpawnDelay = float.Parse(values[3]);
        //maxSpawnDelay = float.Parse(values[4]);
        standardPoints = int.Parse(values[2]);
        standardHits = int.Parse(values[3]);
        bonusPoints = int.Parse(values[4]);
        bonusHits = int.Parse(values[5]);
        freezerSeconds = float.Parse(values[6]);
        speedupSeconds = float.Parse(values[7]);
        speedupFactor = float.Parse(values[8]);
        standardBallSpawnProbability = float.Parse(values[9]) / 100;
        bonusBallSpawnProbability = float.Parse(values[10]) / 100;
        freezerPickupSpawnProbability = float.Parse(values[11]) / 100;
        speedupPickupSpawnProbability = float.Parse(values[12]) / 100;
        easyBallImpulseForce = float.Parse(values[13]);
        mediumBallImpulseForce = float.Parse(values[14]);
        hardBallImpulseForce = float.Parse(values[15]);
        easyMinSpawnDelay = float.Parse(values[16]);
        easyMaxSpawnDelay = float.Parse(values[17]);
        mediumMinSpawnDelay = float.Parse(values[18]);
        mediumMaxSpawnDelay = float.Parse(values[19]);
        hardMinSpawnDelay = float.Parse(values[20]);
        hardMaxSpawnDelay = float.Parse(values[21]);
    }
}
