using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	public int myNumber;
	public int left;
	public int right;
	public int up;
	public int down;

	private GameObject selector;
	private Sleector sleec;

	public bool clicked;
	// Use this for initialization
	void Start () {
		selector = GameObject.FindGameObjectWithTag ("selection");
		sleec = selector.GetComponent<Sleector> ();
	}
	
	// Update is called once per frame
	void Update () {
	if (sleec.number == myNumber) {
			selector.transform.position = gameObject.transform.position;

			if (Input.GetButton ("Jump") && !sleec.wait) {
				clicked = true;
				sleec.wait = true;
			}

		
			if (Input.GetAxis ("Horizontal") > 0 && right != 500 && !sleec.wait) {
				sleec.number = right;
				sleec.wait = true;
			}
			if (Input.GetAxis ("Horizontal") < 0 && left != 500 && !sleec.wait) {
				sleec.number = left;
				sleec.wait = true;
			}
			if (Input.GetAxis ("Vertical") > 0 && up != 500 && !sleec.wait) {
				//Debug.Log ("UP");

				sleec.number = up;

				sleec.wait = true;
			}
			if (Input.GetAxis ("Vertical") < 0 && down != 500 && !sleec.wait) {
				//Debug.Log ("down");

				sleec.number = down;
				sleec.wait = true;
			}
		}
	}
}
