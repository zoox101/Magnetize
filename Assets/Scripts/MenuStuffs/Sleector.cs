using UnityEngine;
using System.Collections;

public class Sleector : MonoBehaviour {
	public int number;

	public float timeLimit;
	public  bool wait;

	private bool thing;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (wait && !thing) {
			StartCoroutine ("WaitTime");
			thing = true;
		}
	}

	IEnumerator WaitTime (){
		yield return new WaitForSeconds (timeLimit);
		wait = false;
		thing = false;
	}
}
