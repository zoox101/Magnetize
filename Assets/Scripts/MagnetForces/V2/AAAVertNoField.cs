using UnityEngine;
using System.Collections;

public class AAAVertNoField : Magnet2
{
    protected Vector3[] verts;
	public float overchargeFactor = 10f;
	private Vector3 pointLocation;

    override protected void Start()
    {
		base.Start ();
		//Getting the magnet's mesh
		Mesh magMesh = transform.GetComponent<MeshFilter>().mesh;
		//Converting that mesh into an array of vertices
        verts = magMesh.vertices;
		//Creating the array of vertices
		for (int i=0; i<verts.Length; i++) verts[i] = transform.TransformPoint(magMesh.vertices[i]);
    }

    protected override Vector3 calcForce()
    {

		//If grounded, return a multiple of the normal to the surface
		if (controller.GetGrounded ()) return controller.GetContactNormal () * controller.GetPlayerMagnetism () * getPolarity () * 50f;
        else
        {
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
		return Vector3.Distance(player.transform.position, pointLocation) + overchargeFactor;
    }

	//Returns the direction of each verticy
	override protected Vector3 getDirection()
    {
		Vector3 direction = player.transform.position - pointLocation;
		direction.Normalize();
		return direction;
    }
}