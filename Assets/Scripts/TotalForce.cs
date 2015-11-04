using UnityEngine;
using System.Collections;

public class TotalForce : MonoBehaviour {
    private Vector3 totalForce;
	private Vector3 totalForceOld;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
		if (totalForce.magnitude != 0) {
			totalForceOld = totalForce;
		}
		totalForce = Vector3.zero;
	}


    public void TallyForce(Vector3 force)
    {
        totalForce += force;
    }

    public Vector3 GetTotalForce()
    {
		if (totalForce.magnitude == 0) {
			Debug.Log ("Left field");
			return totalForceOld;
		}
		else {
			return totalForce;
		}
    }
}
