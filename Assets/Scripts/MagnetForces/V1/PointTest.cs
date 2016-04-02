using UnityEngine;
using System.Collections;

//Doesn't use collider
public class PointTest : Magnet 
{
	void Update ()
	{
		applyForce ();
	}

	override protected float getDistance()
	{
		return Vector3.Distance (player.transform.position, magnet.transform.position);
	}
	
	override protected Vector3 getDirection()
	{
		Vector3 direction = (player.transform.position - magnet.transform.position);
		direction.Normalize ();
		return direction;
	}
}
