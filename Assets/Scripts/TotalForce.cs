using UnityEngine;
using System.Collections;

public class TotalForce : MonoBehaviour {
    private Vector3 totalForce;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        totalForce = Vector3.zero;
	}

    public void TallyForce(Vector3 force)
    {
        totalForce += force;
    }

    public Vector3 GetTotalForce()
    {
        return totalForce;
    }
}
