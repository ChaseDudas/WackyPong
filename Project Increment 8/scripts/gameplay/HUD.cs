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

    PlayerWonEvent playerWonEvent = new PlayerWonEvent();
    #endregion

    #region Unity methods

    /// <summary>
	/// Use this for initialization
    /// </summary>
    void Start()
    {
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
        EventManager.AddPlayerWonInvoker(this);
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

        //Checks to see if either player has won
        if (leftScore > ConfigurationUtils.ScoreToWin)
        {
            playerWonEvent.Invoke(ScreenSide.Left, 0);
        }
        else if (rightScore > ConfigurationUtils.ScoreToWin)
        {
            playerWonEvent.Invoke(ScreenSide.Right, 0);
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

    /// <summary>
    /// Adds the player won listener.
    /// </summary>
    /// <param name="listener">Listener.</param>
    public void AddPlayerWonListener(UnityAction<ScreenSide, int> listener)
    {
        playerWonEvent.AddListener(listener);
    }
    #endregion
}
