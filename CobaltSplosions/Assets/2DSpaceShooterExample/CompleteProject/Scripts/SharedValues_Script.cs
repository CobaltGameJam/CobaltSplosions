/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
///
/// This is the SharedValues Script:
/// - Shared Value Script between all other scripts
/// - In-Game & GameOver GUI
///
/// </summary>

using UnityEngine;
using System.Collections;

public class SharedValues_Script : MonoBehaviour
{
	//Public Var
	public GUIText scoreText; 				//GUI Score
    public GUIText timeText;
	public GUIText GameOverText; 			//GUI GameOver
	public GUIText BestScoreText; 			//GUI Final Score
    public GUIText BestTimeText;
	public GUIText ReplayText; 				//GUI Replay

	//Public Shared Var
	public static int score = 0; 			//Total in-game Score
	public static bool gameover = false; 	//GameOver Trigger
    public static float startTime = 0.0f;
    public float currentTime = 0.0f;

	// Use this for initialization
	void Start ()
	{
		gameover = false; 					//return the Gameover trigger to its initial state when the game restart
		score = 0; 							//return the Score to its initial state when the game restart
        startTime = Time.time;
	}

	// Fixed Update is called one per specific time
	void FixedUpdate ()
	{
		scoreText.text = "Score: " + score; 			//Update the GUI Score

        if (gameover != true)
        {
            currentTime = Time.time - startTime;
            timeText.text = $"Time: {currentTime.ToString("0.0")}";

            if (currentTime > GlobalStats_Script.Instance.BestTime)
            {
                GlobalStats_Script.Instance.BestTime = currentTime;
            }
            if (score > GlobalStats_Script.Instance.BestScore)
            {
                GlobalStats_Script.Instance.BestScore = score;
            }
        }

		//Excute when the GameOver Trigger is True
		if (gameover == true)
		{
			GameOverText.text = "GAME OVER"; 			//Show GUI GameOver
			BestScoreText.text = "Best Score: " + GlobalStats_Script.Instance.BestScore;           //Show GUI FinalScore
            BestTimeText.text = "Best Time: " + GlobalStats_Script.Instance.BestTime.ToString("0.0"); 			//Show GUI FinalScore
			ReplayText.text = "PRESS R TO REPLAY"; 		//Show GUI Replay
		}
	}
}
