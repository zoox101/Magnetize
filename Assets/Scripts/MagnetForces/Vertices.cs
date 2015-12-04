using UnityEngine;
using System.Collections;

//Gets closest verticy from the object
public class Vertices : Magnet 
{
    protected Vector3[] verts;
    protected int count;
    protected Vector3 forceSum;
    protected Vector3 direction;
    protected int numVertices;
    protected Mesh magMesh;
	
    public Transform target;
    public float scale;

    protected void Start()
    {
		base.Start ();
        magMesh = target.GetComponent<MeshFilter>().mesh;
        verts = magMesh.vertices;
		for (int i=0; i<verts.Length; i++) verts[i] = target.transform.TransformPoint(magMesh.vertices[count]);

		//Debugging
		Vector3 playerPosition = player.transform.position;
		for(int i=0; i<10; i++) Debug.DrawLine(playerPosition, verts[i], Color.white, 5f);
    }

    protected override void applyForce()
    {
		//If grounded
        if (controller.GetGrounded())
        {
			//Helping camera
            playerTotalForce.TallyForce(controller.GetContactNormal() * 50f, magnetStrength);
			//Adding force
            body.AddForce(controller.GetPlayerMagnetism() * controller.GetContactNormal() * 50f);
        }
        else
        {
			magnetForce = Vector3.zero;
			float vertStrength = magnetStrength/verts.Length;
			for(int i=0; i<verts.Length; i++)
			{
				Vector3 vertForce = (vertStrength * getVerticesDirection(verts[count]) / (Mathf.Pow(getVerticesDistance(verts[count]) + 10, magnetScaling)));
				float maxVertForce = maxForce/verts.Length;
				//Capping force
				if(vertForce.magnitude > maxVertForce) vertForce = vertForce.normalized * maxVertForce;
				magnetForce += vertForce;
			}

			//Capping force
            //if (magnetForce.magnitude > maxForce) magnetForce = magnetForce.normalized * maxForce;
            playerTotalForce.TallyForce(magnetForce, magnetStrength);
            magnetForce *= controller.GetPlayerMagnetism();
            body.AddForce(magnetForce);
        }
    }

    protected Vector3 getVerticesDirection(Vector3 pointLocation)
    {
        direction = player.transform.position - pointLocation;
        direction.Normalize();
        return direction;
    }

    protected float getVerticesDistance(Vector3 pointLocation)
    {
        return Vector3.Distance(player.transform.position, pointLocation);
    }

    override protected float getDistance()
    {
        return -1;
    }

	override protected Vector3 getDirection()
    {
        return Vector3.zero;
    }

    void FixedUpdate()
    {
        count = 0;
        while (count < 10)
        {
            Debug.DrawLine(player.transform.position, target.transform.TransformPoint(magMesh.vertices[count]), Color.white);
            count++;
        }
    }
}
