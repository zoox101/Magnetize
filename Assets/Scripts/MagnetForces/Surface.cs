using UnityEngine;
using System.Collections;

public class Surface :  Magnet 
{
	override protected float getDistance()
	{
		return Vector3.Distance(player.transform.position, magnet.GetComponent<Collider>().ClosestPointOnBounds(player.transform.position));
	}

	override protected Vector3 getDirection()
	{
		Vector3 direction = (player.transform.position - magnet.GetComponent<Collider> ().ClosestPointOnBounds (player.transform.position));
		direction.Normalize ();
		return direction;
	}
}
