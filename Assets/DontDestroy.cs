using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour 
{
	public static DontDestroy instance;
	public static Transform thatTransform;

	// Use this for initialization
	void Awake () 
	{
		DontDestroyOnLoad (this);
		if (instance == null) {
			instance = this;
			Debug.Log ("Test: " + instance);
		} else if (this == instance) {
			Debug.Log ("Hit");
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Debug.Log ("Hit1");
			thatTransform = transform;
			Destroy(this.gameObject);
		}
	}

	void OnLevelWasLoaded()
	{
		Debug.Log ("Hit2");
		if(thatTransform != null)
		{
			transform.position = thatTransform.position;
			transform.rotation = thatTransform.rotation;
		}
	}
}
