using UnityEngine;
using System.Collections;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{

	private static T _instance;
	public static T Instance
	{
		get{ 
			if (_instance == null) {
				_instance = (T)GameObject.FindObjectOfType (typeof(T));
				if (_instance == null) {
					GameObject obj = new GameObject (typeof(T).Name);
					_instance = obj.AddComponent<T> ();
                    GameObject parent = GameObject.Find("MonoSingleton");
                    if (parent == null)
                    {
                        parent = new GameObject("MonoSingleton");
                    }
                    _instance.transform.parent = parent.transform;
					DontDestroyOnLoad(parent);
				}
			}
			return _instance;
		}
		set { throw new System.NotImplementedException(); }
	}
}

