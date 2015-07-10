using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	/** Instance */
	static EventManager _instance = null;
	public static EventManager instance
	{
		get
		{
			if(!_instance){
				_instance = FindObjectOfType(typeof(EventManager)) as EventManager;
				
				if(!_instance)
				{
					var obj = new GameObject("EventManager");
					_instance = obj.AddComponent<EventManager>();
				}
			}
			return _instance;
		}
	}

	/** Starts game */
	public delegate void OnStartHandler();
	public static event OnStartHandler onStartGame;

	/** pause handler for game */
	public delegate void OnPauseHandler(bool openMenu);
	public static event OnPauseHandler onPauseGame;

	/** resume handler */
	public delegate void OnResumeHandler(bool openMenu);
	public static event OnResumeHandler onResumeGame;

	/** Level complete handler */
	public delegate void OnLevelCompleteDelegate(int winner);
	public static event OnLevelCompleteDelegate onLevelComplete;

	/** Level failed handler */
	public delegate void OnLevelFailDelegate();
	public static event OnLevelFailDelegate onLevelFail;

	/** Level load next handler */
	public delegate void OnLoadNextLevel();
	public static event OnLoadNextLevel onLoadNextLevel;

	/** Level reload handler */
	public delegate void OnReloadLevel();
	public static event OnReloadLevel onReloadLevel;

	public void OnStart ()
	{
		if(onStartGame!=null)onStartGame();
	}
	public void OnPause(bool openMenu)
	{
		if(onPauseGame != null) onPauseGame(openMenu);
	}
	public void OnResume(bool openMenu)
	{
		if(onResumeGame != null) onResumeGame(openMenu);
	}
	public void OnLevelCompleted(int winner)
	{
        if (onLevelComplete != null) onLevelComplete(winner);
	}
	public void OnLevelFailed()
	{
		if(onLevelFail != null) onLevelFail();
	}
	public void OnLoadNextlevel()
	{
		if(onLoadNextLevel != null) onLoadNextLevel();
	}
	public void OnReloadCurrentLevel()
	{
		if(onReloadLevel != null) onReloadLevel();
	}
}
