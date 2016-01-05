using UnityEngine;
using System.Collections;

public class PlayerAttractor : MonoBehaviour {


    public GameObject player;
    public GameObject innerMagnet;
    private Transform playerLocation;
    private Rigidbody attractorBody;
    private Transform attractorLocation;
    private Collider innerCollider;
    private Vector3 toPlayer;
    private Ray rayToPlayer;
    private Vector3 lastToPlayer;
    private RaycastHit hit;
    private Vector3 newLocation;
	// Use this for initialization
	void Start () 
    {
        playerLocation = player.GetComponent<Transform>();
        attractorLocation = this.GetComponent<Transform>();
        attractorBody = this.GetComponent<Rigidbody>();
        innerCollider = innerMagnet.GetComponent<Collider>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        //lastToPlayer = toPlayer;
        toPlayer = player.transform.position-this.transform.position;
        //toPlayer.Normalize();
        //rayToPlayer = new Ray(transform.position, toPlayer);
        //innerCollider.Raycast(rayToPlayer, out hit, 50f);
        //Physics.SphereCast(rayToPlayer,.5f,out hit);
        //Debug.Log(hit.point);

        toPlayer.Normalize();
        toPlayer *= 3f;
        attractorBody.AddForce(toPlayer,ForceMode.VelocityChange);
        //newLocation = hit.point - attractorLocation.position;
        //newLocation = newLocation.normalized * (newLocation.magnitude - 0f);
        //attractorLocation.position = hit.point;
        
	}
}
