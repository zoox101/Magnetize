using UnityEngine;
using System.Collections;

public abstract class Magnet : MonoBehaviour {

	//Editable Values
	public float magnetScaling = 2.0f;
	public float magnetStrength = 1000f; //k
	public float maxForce = 500f; //StrengthLimit
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
		{
			magnetForce = magnetStrength * getDirection () / (Mathf.Pow(getDistance (), magnetScaling));
			player.GetComponent<TotalForce>().TallyForce(magnetForce, magnetStrength);
			magnetForce *= controller.GetPlayerMagnetism();
			if (magnetForce.magnitude > maxForce) magnetForce = magnetForce.normalized * maxForce;
			body.AddForce(magnetForce);
		}
	}

	protected abstract float getDistance () ;
	protected abstract Vector3 getDirection();

}
