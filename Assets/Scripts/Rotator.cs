using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
	public int direction = 1;
	public float speed = 1;

	void Update () 
    {
		transform.Rotate (new Vector3(speed * 15 * direction, speed * 30 * direction, speed * 45 * direction) * Time.deltaTime);	
	}
}
