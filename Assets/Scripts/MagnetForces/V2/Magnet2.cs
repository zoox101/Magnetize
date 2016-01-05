using UnityEngine;
using System.Collections;

public abstract class Magnet2 : MonoBehaviour {

	//Editable Values
	public float magnetScaling = 2.0f;
	public float magnetStrength = 2000f; //k 1000
	public float maxForce = 200f; //StrengthLimit 500

	protected GameObject player;
	protected GameObject magnet;

	//protected Vector3 magnetForce;
	protected Rigidbody body;
	protected PlayerController controller;
    protected TotalForce playerTotalForce;

	virtual protected void Start()
	{
		player = GameObject.Find ("Player");
		magnet = gameObject;
        body = player.GetComponent<Rigidbody>();
		controller = player.GetComponent<PlayerController> ();
        playerTotalForce = player.GetComponent<TotalForce>();
	}

	virtual protected void Update()
	{
		Vector3 magnetForce = calcForce ();
		applyForce (magnetForce);
	}

	//Returns the force from the magnet piece
	virtual protected Vector3 calcForce(float effectiveStrength)
	{
		//Coulomb's Law
		Vector3 magnetForce = effectiveStrength * getDirection () / (Mathf.Pow(getDistance (), magnetScaling));
		//Factoring in the user's polarity
		magnetForce *= controller.GetPlayerMagnetism();;
		return magnetForce;
	}

	//Overriding the calcForce method for classes that don't change the magnet strength
	virtual protected Vector3 calcForce(){return calcForce (magnetStrength);}

	//Applies the force to the player
	virtual protected void applyForce(Vector3 magnetForce)
	{
		if (magnetForce.magnitude > maxForce) magnetForce = magnetForce.normalized * maxForce;
		playerTotalForce.TallyForce(magnetForce, magnetStrength);
		body.AddForce(magnetForce);
	}

	//Returns the polarity of the current object
	protected int getPolarity()
	{
		if (magnetStrength >= 0) return 1;
		else return -1;
	}

	//Returns the distance to each magnet
	protected abstract float getDistance () ;
	//Retuns the direction of each magnet from the player
	protected abstract Vector3 getDirection();
}
