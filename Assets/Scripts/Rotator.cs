using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
	//Primary Rotation
	private Vector3 primary = new Vector3 (0, 0, 1);
	public float speed = 100f;

	//Drift Rotation
	public bool drift = false;
	public float driftSpeed = 10f;
	private Vector3 secondary = new Vector3 (1, 0, 0);

	//Degree counter
	private float degrees = 0f;

	void Update () 
    {
		//Getting the current rotation of the object
		Vector3 position = transform.rotation.eulerAngles;

		//If drift is enabled
		if (drift)
		{
			//Rotate one direction on one side
			if(degrees <= 180) rotate (secondary, driftSpeed);
			//Rotate the other direction on the other
			else rotate (-secondary, driftSpeed);
		}

		//Rotate the object and track the number of degrees z turned
		degrees += rotate (primary, speed);
		//Reset degree counter after a full rotation
		if (degrees > 360f) degrees -= 360f;
	}

	//Rotates the object a direction at a given velocity
	float rotate(Vector3 rotation, float velocity)
	{
		Vector3 spin = rotation.normalized * Time.deltaTime * velocity;
		transform.Rotate (spin);
		return spin.z;
	}
}
