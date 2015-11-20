using UnityEngine;
using System.Collections;

public class Sphere : Magnet
{
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

    override protected void applyForce()
    {
        magnetForce = magnetStrength * getDirection() / (Mathf.Pow(getDistance(), magnetScaling));
        playerTotalForce.TallyForce(magnetForce, magnetStrength);
        magnetForce *= controller.GetPlayerMagnetism();
        if (magnetForce.magnitude > maxForce)
        {
            magnetForce = magnetForce.normalized * maxForce;
        }
        body.AddForce(magnetForce);
    }
}
