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
        body = player.GetComponent<Rigidbody>();
        controller = player.GetComponent<PlayerController>();
        playerTotalForce = player.GetComponent<TotalForce>();
        magMesh = target.GetComponent<MeshFilter>().mesh;
        verts = magMesh.vertices;

        count = 0;
        while (count < 10)
        {
            Debug.DrawLine(player.transform.position, target.transform.TransformPoint(magMesh.vertices[count]), Color.white, 5f);
            count++;
        }
        count = 0;
        while (count < verts.Length)
        {
            verts[count] = target.transform.TransformPoint(magMesh.vertices[count]);
            count++;
        }
    }
    protected override void applyForce()
    {
        //throw new System.NotImplementedException();
        if (controller.GetGrounded())
        {
            playerTotalForce.TallyForce(controller.GetContactNormal() * 50f, magnetStrength);
            body.AddForce(controller.GetPlayerMagnetism() * controller.GetContactNormal() * 50f);
        }
        else
        {
            count = 0;
            forceSum = Vector3.zero;
            while (count < verts.Length)
            {

                forceSum += (magnetStrength * getVerticesDirection(verts[count]) / (Mathf.Pow(getVerticesDistance(verts[count]), magnetScaling))) / 100;
                count++;
            }
            magnetForce = forceSum;
            if (magnetForce.magnitude > maxForce)
            {
                magnetForce = magnetForce.normalized * maxForce;
            }
            playerTotalForce.TallyForce(magnetForce, magnetStrength);
            magnetForce *= controller.GetPlayerMagnetism();
            body.AddForce(magnetForce);
        }
    }
    protected Vector3 getVerticesDirection(Vector3 pointLocation)
    {
        //throw new System.NotImplementedException();
        //direction = player.transform.position - pointLocation*scale - magnet.transform.position;
        direction = player.transform.position - pointLocation;
        direction.Normalize();
        return direction;
    }
    protected float getVerticesDistance(Vector3 pointLocation)
    {
        //throw new System.NotImplementedException();
        //return Vector3.Distance(player.transform.position, pointLocation*scale + magnet.transform.position);
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
