using UnityEngine;
using System.Collections;

public class EventReceiverBehaviour : MonoBehaviour,IEventReceicer {
    public bool IsPlaying
    {
        get;
        protected set;
    }
	
    public virtual void OnEnable () {
		EventManager.onStartGame += OnStart;
		EventManager.onPauseGame += OnPause;
		EventManager.onResumeGame += OnResume;
		EventManager.onLevelFail += OnLevelFail;
		EventManager.onLevelComplete += OnLevelComplete;
		EventManager.onLoadNextLevel += OnLoadNextLevel;
		EventManager.onReloadLevel += OnReloadLevel;
	}
	
	public virtual void OnDisable () {
		EventManager.onStartGame -= OnStart;
		EventManager.onPauseGame -= OnPause;
		EventManager.onResumeGame -= OnResume;
		EventManager.onLevelFail -= OnLevelFail;
		EventManager.onLevelComplete -= OnLevelComplete;
		EventManager.onLoadNextLevel -= OnLoadNextLevel;
		EventManager.onReloadLevel -= OnReloadLevel;
	}

	#region IEventReceicer implementation

	public virtual void OnStart (){IsPlaying = true;}
	
	public virtual void OnResume (bool openMenu){IsPlaying = true;}
	
	public virtual void OnPause (bool openMenu){IsPlaying = false;}
	
	public virtual void OnLevelComplete (int winner){IsPlaying = false;}
	
	public virtual void OnLevelFail (){IsPlaying = false;}
	
	public virtual void OnLoadNextLevel (){}
	
	public virtual void OnReloadLevel (){}

	#endregion
}

public interface IEventReceicer{
	void OnStart();
	void OnResume(bool openMenu);
	void OnPause(bool openMenu);
	void OnLevelComplete(int winner);
	void OnLevelFail();
	void OnLoadNextLevel();
	void OnReloadLevel();
}
