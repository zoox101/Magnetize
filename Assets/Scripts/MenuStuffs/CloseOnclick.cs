using UnityEngine;
using System.Collections;

public class CloseOnclick : MonoBehaviour {
	private Selectable sl;
	
	//public int levelNumber;
	// Use this for initialization
	void Start () {
		sl = GetComponent<Selectable> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (sl.clicked) {
			//Debug.Log ("worked");
			Application.Quit();

		}
	}
}
