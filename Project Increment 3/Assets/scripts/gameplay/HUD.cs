using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //scoring and hit support
    [SerializeField]
    Text scoreText;
    float lscore;
    float rscore;
    const string ScorePrefix = " :: ";

    [SerializeField]
    Text leftHits;
    float lefth;
    const string LeftHitsPrefix = "Hits: ";

    [SerializeField]
    Text rightHits;
    float righth;
    const string RightHitsPrefix = "Hits: ";



    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = lscore.ToString() + ScorePrefix + rscore;


        leftHits.text = LeftHitsPrefix + lefth.ToString();
        rightHits.text = RightHitsPrefix + righth.ToString();

    }

    /// <summary>
    ///     Add hits for each paddle
    /// </summary>
    public void AddHits(ScreenSide side, float nHitsToAdd)
    {
        if (side == ScreenSide.Left)
        {
            lefth += nHitsToAdd;
            leftHits.text = LeftHitsPrefix + lefth.ToString();
        }
        else if (side == ScreenSide.Right)
        {
            righth += nHitsToAdd;
            rightHits.text = RightHitsPrefix + righth.ToString();
        }
    }

    /// <summary>
    ///     Control score of game
    /// </summary>
    public void AddScore(ScreenSide scoringSide, float amountAdded)
    {
        switch (scoringSide)
        {
            case ScreenSide.Left:
                lscore += amountAdded;
                break;
            case ScreenSide.Right:
                rscore += amountAdded;
                break;
        }
        scoreText.text = lscore.ToString() + ScorePrefix + rscore;

    }
}
