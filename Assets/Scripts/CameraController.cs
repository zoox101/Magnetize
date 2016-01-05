using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	
    public float topCamera = 1.2f;
    public float backCamera = 1.35f;
    public float posDamping = 2f;

	private GameObject player;
    private Vector3 offset;
    private float offsetMag;
    private Quaternion newLook;
    private Quaternion oldLook;
    private Vector3 totalForceDirection;
    private Vector3 curryVelocity;
    private PlayerController controller;
    
    void Start()
    {
		player = GameObject.Find ("Player");
        transform.LookAt(player.transform.position, Vector3.up);
        newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
		controller = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        oldLook = newLook;
    }

    void LateUpdate()
    {
		Vector3 playerVelocity = player.GetComponent<Rigidbody> ().GetPointVelocity (player.transform.position);
		Vector3 playerTotalForce = player.GetComponent<TotalForce> ().GetTotalForce ();
		PlayerController controller = player.GetComponent<PlayerController> ();

		//Getting the direction of the player's motion
        offset = playerVelocity;
		//Getting the magnitude of the motion
        offsetMag = offset.magnitude;
		//Telling the camera to follow the player plus an offset coefficient
        offset = offset.normalized * (backCamera + offsetMag/10);

		//Offsets the camera above the player
		offset += playerTotalForce.normalized * controller.GetPlayerMagnetism ();
		//No idea what this does
		offset *= (5 + (playerVelocity.magnitude/10));
		//Multiplies by a constant
        offset *= topCamera;
		//Flips everything
		offset *= -1;

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref curryVelocity, .9f,150f,Time.deltaTime*posDamping);
		totalForceDirection = playerTotalForce;
        totalForceDirection *= 1;
        totalForceDirection.Normalize();

		if (!controller.GetGrounded())
        {
            newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
        }
        else
        {
			//If you're on the ground set camera behind the player with the ground below
			newLook.SetLookRotation(player.transform.position - transform.position, controller.GetContactNormal());
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newLook, Time.deltaTime * Quaternion.Angle(transform.rotation,newLook)*5f);
    }
}

