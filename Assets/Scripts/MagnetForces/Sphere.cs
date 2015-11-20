using UnityEngine;
using System.Collections;

//Gets the center point of the object
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
}
