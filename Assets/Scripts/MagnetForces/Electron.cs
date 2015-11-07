using UnityEngine;
using System.Collections;

public class Electron : Magnet 
{
	public static GameObject[] electrons = new GameObject[0];
	private float electronStrength = 0.1f;
	private Rigidbody electronBody;

	void OnTriggerStay(Collider other){}

	void Start()
	{
		GameObject[] electronsCopy = new GameObject[electrons.Length + 1];
		for (int i=0; i<electrons.Length; i++) {
			electronsCopy[i] = electrons[i];
		}
		electronsCopy[electrons.Length] = player;
		electrons = electronsCopy;
		body = player.GetComponent<Rigidbody>();
	}

	void Update () 
	{
		applyForce ();
	}

	override protected float getDistance()
	{
		return Vector3.Distance(player.transform.position, magnet.GetComponent<Collider>().ClosestPointOnBounds(player.transform.position));
	}
	
	override protected Vector3 getDirection()
	{
		Vector3 direction = (player.transform.position - magnet.GetComponent<Collider> ().ClosestPointOnBounds (player.transform.position));
		direction.Normalize ();
		return direction;
	}

	protected float getDistance(GameObject electron)
	{
		return Vector3.Distance (player.transform.position, electron.transform.position);
	}

	protected Vector3 getDirection(GameObject electron)
	{
		Vector3 direction = (player.transform.position - electron.transform.position);
		direction.Normalize ();
		return direction;
	}

	override protected void applyForce()
	{
		for(int i=0; i<electrons.Length; i++)
		{
			if(getDistance (electrons[i]) != 0)
			{
				magnetForce = electronStrength * getDirection (electrons[i]) / (Mathf.Pow(getDistance (electrons[i]), magnetScaling));
				body.AddForce(magnetForce);
			}
		}

		magnetForce = magnetStrength * -1 * getDirection () / (Mathf.Pow (getDistance (), magnetScaling));
		body.AddForce(magnetForce);
	}






}
