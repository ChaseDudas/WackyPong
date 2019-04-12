using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gameplay manager
/// </summary>
public class GameplayManager : MonoBehaviour
{
    GameOverMessage gameover;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        EventManager.AddPlayerWonListener(QueueGameOver);
    }
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        // pause game on escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.Play(AudioClipName.Pause);
            MenuManager.GoToMenu(MenuName.Pause);
        }

    }

    void QueueGameOver(ScreenSide side, int points)
    {
       //Object.Instantiate(Resources.Load("GameOverMessage"));
        GameOverMessage instance = Instantiate(Resources.Load("GameOverMessage", typeof(GameOverMessage))) as GameOverMessage;
        instance.SetWinner(side);
        AudioManager.Play(AudioClipName.GameOver);
    }
}
