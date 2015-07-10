using UnityEngine;
using System.Collections;

public class GameController : BaseGameController 
{
	/** Instance */
	static GameController _instance = null;
	public static GameController instance{
		get{
			if(!_instance){
				_instance = FindObjectOfType(typeof(GameController)) as GameController;
				
				if(!_instance){
					var obj = new GameObject("GameController");
					_instance = obj.AddComponent<GameController>();
				}
			}
			return _instance;
		}
	}
	/** Singleton Option */
	public bool isSingleton = false;
	public bool startWithStart = false;
	public bool handlePauseWhenAppGoesBackground = true;
	#region UNITY_FUNCS
	void Awake()
	{
		if(isSingleton)DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
#if UNITY_ANDROID || UNITY_IOS
		Application.targetFrameRate = 60;
#endif
		if(startWithStart)StartGame();
	}
	void OnApplicationPause(bool paused)
	{
        if (gameState.Equals(GameState.Playing) && handlePauseWhenAppGoesBackground)
        {
            SetGameState(GameState.Paused);
            EventManager.instance.OnPause(true);
        }
	}
	void OnApplicationQuit(){_instance = null;}
	#endregion

	#region PUBLIC_FUNCS
	/** Called with start button */
	public void StartGame()
	{
		SetGameState(GameState.Playing);
        EventManager.instance.OnStart();
	}
	/** called when want pause */
	public void HandlePause(bool openMenu)
	{
		if(gameState.Equals(GameState.Playing))
		{
			SetGameState(GameState.Paused);
            EventManager.instance.OnPause(openMenu);
		}
		else if(gameState.Equals(GameState.Paused))
		{
			SetGameState(GameState.Playing);
            EventManager.instance.OnResume(openMenu);
		}
	}
	/** Called when finished */
	public void EndGame(int winner)
	{
		SetGameState(GameState.Ended);
        EventManager.instance.OnLevelCompleted(winner);
	}
	#endregion

}
