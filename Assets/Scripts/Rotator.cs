using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
	public int direction = 1;

	void Update () 
    {
		transform.Rotate (new Vector3(15 * direction, 30 * direction, 45 * direction) * Time.deltaTime);	
	}
}
