using UnityEngine;
using System.Collections;

//Gets the center point of the object
public class Sphere : Magnet2
{
	/*
	override protected Vector3 calcForce()
	{
		//If grounded, return a multiple of the normal to the surface
		if (controller.GetGrounded ()) 
		{
			if(gameObject == controller.getContactObject())
			{
				return controller.GetContactNormal () * controller.GetPlayerMagnetism () * getPolarity () * 5000f;
			}
			else
			{
				return new Vector3(0, 0, 0);
			}
		}
		else
		{
			return base.calcForce();
		}
	}
	*/

    override protected float getDistance()
    {
        return Vector3.Distance(player.transform.position, magnet.transform.position);
    }

    override protected Vector3 getDirection()
    {
        Vector3 direction = (player.transform.position - magnet.transform.position);
        direction.Normalize();
        return direction;
    }
}
