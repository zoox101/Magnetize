using UnityEngine;
using System.Collections;

public class Point : Magnet 
{
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

    override protected void applyForce()
    {
        magnetForce = magnetStrength * getDirection() / (Mathf.Pow(getDistance(), magnetScaling));
        //playerTotalForce.TallyForce(magnetForce, magnetStrength);
        //magnetForce *= controller.GetPlayerMagnetism();
        if (magnetForce.magnitude > maxForce)
        {
            magnetForce = magnetForce.normalized * maxForce;
        }
        if (controller.GetGrounded())
        {
            playerTotalForce.TallyForce(controller.GetContactNormal() * 50f, magnetStrength);
            body.AddForce(controller.GetPlayerMagnetism() * controller.GetContactNormal() * 50f);
            //body.AddForce(magnetForce.normalized * 50f);
        }
        else
        {
            playerTotalForce.TallyForce(magnetForce, magnetStrength);
            magnetForce *= controller.GetPlayerMagnetism();
            body.AddForce(magnetForce);
        }
    }
}
