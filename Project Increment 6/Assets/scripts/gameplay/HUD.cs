using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    int leftScore = 0;
    int rightScore = 0;
    Text scoreText;

    // hits text support
    const string HitsPrefix = "Hits: ";
    int leftHits = 0;
    Text leftHitsText;
    int rightHits = 0;
    Text rightHitsText;

    #endregion

    #region Unity methods

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextGameObject.GetComponent<Text>();
        leftHitsText = leftHitsTextGameObject.GetComponent<Text>();
        rightHitsText = rightHitsTextGameObject.GetComponent<Text>();

        //Add listener
        EventManager.AddListener(AddPoints);
        EventManager.addListener(AddHits);
    }

    #endregion

    #region Public methods

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
