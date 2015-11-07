using UnityEngine;
using System.Collections;

public class StartGameLevel : MonoBehaviour {
	private Selectable sl;

	public int levelNumber;
	// Use this for initialization
	void Start () {
		sl = GetComponent<Selectable> ();
	}
	
	// Update is called once per frame
	void Update () {
	if (sl.clicked) {
			Application.LoadLevel (levelNumber);
		}
	}
}
