using UnityEngine;
using System.Collections;

public class AAAVertNoField : Magnet2
{
	public float proximityFactor = 10f;
    protected Vector3[] verts;
	private Vector3 pointLocation;

    override protected void Start()
    {
		base.Start ();
		//Getting the magnet's mesh
		Mesh magMesh = transform.GetComponent<MeshFilter>().mesh;
		//Converting that mesh into an array of vertices
        verts = magMesh.vertices;
		//Creating the array of vertices
		for (int i=0; i<verts.Length; i++) 
			verts[i] = transform.TransformPoint(magMesh.vertices[i]);
    }

	override protected Vector3 calcForce() {
		//If the player is grounded...
		if (controller.GetGrounded ()) {
			//If the player is grounded to this object...
			if(gameObject == controller.getContactObject()) {
				return controller.GetContactNormal () * controller.GetPlayerMagnetism () * getPolarity () * 50f;
			}
			//Otherwise its grounded to something else...
			else {
				return new Vector3(0, 0, 0);
			}
		}
		//If the player isn't grounded...
        else {
			//Initilizing magnet force counter
			Vector3 magnetForce = Vector3.zero;
			//Defining the vertStrength
			float vertStrength = magnetStrength/verts.Length;
			//Looping over the array and calculating the force to each verticy
			for(int i=0; i<verts.Length; i++) 
			{
				pointLocation = verts[i];
				magnetForce += base.calcForce(vertStrength);
			}
			return magnetForce;
        }
    }

	//Returns the distance to each verticy
    override protected float getDistance()
    {
		return Vector3.Distance(player.transform.position, pointLocation) + proximityFactor;
    }

	//Returns the direction of each verticy
	override protected Vector3 getDirection()
    {
		Vector3 direction = player.transform.position - pointLocation;
		direction.Normalize();
		return direction;
    }
}