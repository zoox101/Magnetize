using UnityEngine;
using System.Collections;

public abstract class Magnet : MonoBehaviour {

	//Editable Values
	public float magnetScaling = 2.0f;
	public float magnetStrength = 10f; //k 1000
	public float maxForce = 10f; //StrengthLimit 500
	public GameObject magnet;
	public GameObject player;

	protected Vector3 magnetForce;
	protected Rigidbody body;
	protected PlayerController controller;

	void Start()
	{
		body = player.GetComponent<Rigidbody>();
		controller = player.GetComponent<PlayerController> ();
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			applyForce();
	}

	virtual protected void applyForce()
	{
		magnetForce = magnetStrength * getDirection () / (Mathf.Pow(getDistance (), magnetScaling));
		player.GetComponent<TotalForce>().TallyForce(magnetForce, magnetStrength);
		magnetForce *= controller.GetPlayerMagnetism();
		if (magnetForce.magnitude > maxForce) magnetForce = magnetForce.normalized * maxForce;
		body.AddForce(magnetForce);
	}
	
	protected abstract float getDistance () ;
	protected abstract Vector3 getDirection();

}
