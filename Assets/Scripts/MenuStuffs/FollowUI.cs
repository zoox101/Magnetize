using UnityEngine;
using System.Collections;

public class FollowUI : MonoBehaviour {
	public GameObject follow;
	public Vector3 editDist;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (follow.transform.position.x + editDist.x, follow.transform.position.y + editDist.y, follow.transform.position.z + editDist.z);
	}
}
