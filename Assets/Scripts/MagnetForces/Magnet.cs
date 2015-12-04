using UnityEngine;
using System.Collections;

public abstract class Magnet : MonoBehaviour {

	//Editable Values
	public float magnetScaling = 2.0f;
	public float magnetStrength = 500f; //k 1000
	public float maxForce = 200f; //StrengthLimit 500

	protected GameObject player;
	protected GameObject magnet;

	protected Vector3 magnetForce;
	protected Rigidbody body;
	protected PlayerController controller;
    protected TotalForce playerTotalForce;

	protected void Start()
	{
		player = GameObject.Find ("Player");
		magnet = gameObject;
        body = player.GetComponent<Rigidbody>();
		controller = player.GetComponent<PlayerController> ();
        playerTotalForce = player.GetComponent<TotalForce>();
	}

	protected void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			applyForce();
	}

	virtual protected void applyForce()
	{
		magnetForce = magnetStrength * getDirection () / (Mathf.Pow(getDistance (), magnetScaling));
		playerTotalForce.TallyForce(magnetForce, magnetStrength);
		magnetForce *= controller.GetPlayerMagnetism();
        if (magnetForce.magnitude > maxForce)
        {
            magnetForce = magnetForce.normalized * maxForce;
        }
        body.AddForce(magnetForce);
	}
	
	protected abstract float getDistance () ;
	protected abstract Vector3 getDirection();

}
