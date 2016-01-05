using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour 
{
	public int kick = 2000;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			Rigidbody player = other.GetComponent<Rigidbody>();
			Vector3 force = new Vector3(0, 0, kick);
			player.AddForce(force);
			Debug.Log("Goal Hit");
			this.gameObject.SetActive(false);
			//Load the next level
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
}
