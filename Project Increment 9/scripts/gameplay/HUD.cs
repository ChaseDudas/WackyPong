using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region Fields

    [SerializeField]
    GameObject scoreTextGameObject;
    [SerializeField]
    GameObject leftHitsTextGameObject;
    [SerializeField]
    GameObject rightHitsTextGameObject;

    // score text support
    const string ScoreSeparator = " - ";
    static int leftScore = 0;
    static int rightScore = 0;
    static Text scoreText;

    // hits text support
    const string HitsPrefix = "Hits: ";
    static int leftHits = 0;
    static Text leftHitsText;
    static int rightHits = 0;
    static Text rightHitsText;

    // events invoked by the class
    PlayerWonEvent playerWonEvent = new PlayerWonEvent();
    const int PointsPerGame = 5;

    #endregion

    #region Unity methods

    /// <summary>
	/// Use this for initialization
    /// </summary>
    void Start()
    {
        // initialize counts
        leftScore = 0;
        rightScore = 0;
        leftHits = 0;
        rightHits = 0;

        scoreText = scoreTextGameObject.GetComponent<Text>();
        leftHitsText = leftHitsTextGameObject.GetComponent<Text>();
        rightHitsText = rightHitsTextGameObject.GetComponent<Text>();

        // add as listener for various events
        EventManager.AddBallLostListener(AddPoints);
        EventManager.AddHitsAddedListener(AddHits);

        // add as invoker for player won event
        EventManager.AddPlayerWonInvoker(this);
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Adds the given listener for the player won event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddPlayerWonListener(UnityAction<ScreenSide> listener)
    {
        playerWonEvent.AddListener(listener);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Adds the given points to the given side
    /// </summary>
    /// <param name="side">screen side</param>
    /// <param name="points">points to add</param>
    void AddPoints(ScreenSide side, int points)
    {
        // add points and change text
        if (side == ScreenSide.Left)
        {
            leftScore += points;
        }
        else
        {
            rightScore += points;
        }
        scoreText.text = leftScore + ScoreSeparator + rightScore;

        // invoke player won event as appropriate
        if (leftScore >= PointsPerGame)
        {
            playerWonEvent.Invoke(ScreenSide.Left);
        }
        else if (rightScore >= PointsPerGame)
        {
            playerWonEvent.Invoke(ScreenSide.Right);
        }
    }

    /// <summary>
    /// Adds the given hits to the given side
    /// </summary>
    /// <param name="side">screen side</param>
    /// <param name="hits">hits to add</param>
    void AddHits(ScreenSide side, int hits)
    {
        // add hits and change text
        if (side == ScreenSide.Left)
        {
            leftHits += hits;
            leftHitsText.text = HitsPrefix + leftHits;
        }
        else
        {
            rightHits += hits;
            rightHitsText.text = HitsPrefix + rightHits;
        }
    }

    #endregion
}
