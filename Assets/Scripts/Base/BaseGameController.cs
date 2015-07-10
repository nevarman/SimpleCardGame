using UnityEngine;
using System.Collections;
using System;

public class BaseGameController : MonoBehaviour {
	#region GameState
	/** Game state */
	public GameState gameState = GameState.OnStart;

	/** Changes game state */
	public void SetGameState(GameState gState)
	{
		gameState = gState;
	}
	#endregion

	#region Score
	/** sets high score int if its bigger than current one */
	public virtual void SetHighScoreLocally(int score)
	{
		if(GetHighScoreLocal() < score){
			PlayerPrefs.SetInt("SCORE",score);
			SendScoreToServer();
		} 
	}

	/** Adds to highscore */
	public virtual void AddToHighScoreLocal(int score)
	{
		int s = GetHighScoreLocal();
		s += score;
		SetHighScoreLocally(s);
	}

	/** Gets integer high score */
	public int GetHighScoreLocal()
	{
		return PlayerPrefs.GetInt("SCORE",0);
	}

	/** Sends score to server if any */
	public virtual void SendScoreToServer(){Debug.LogWarning("Server side not implemented yet!");}
	#endregion

	#region Game_Plays
	public virtual void IncreaseNumberOfPlays(int doActionOn, System.Action onDoAction)
	{
		int num = GetNumberOfPlays();
		num++;
		if(num == doActionOn && onDoAction != null)
			onDoAction();
		PlayerPrefs.SetInt("NUM",num);
	}

	public virtual int GetNumberOfPlays()
	{
		return PlayerPrefs.GetInt("NUM",0);
	}

	#endregion
}
public enum GameState
{
	OnStart,
	Idle,
	Paused,
	Playing,
	Ended
}
