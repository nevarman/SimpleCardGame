using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;

	public static T instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = (T) FindObjectOfType(typeof(T));
				
				if (_instance == null)
				{
					Debug.LogWarning("An instance of " + typeof(T) + 
					               " is needed in the scene, but there is none.");
					var obj = new GameObject("GameobjectInstance");
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}
}
