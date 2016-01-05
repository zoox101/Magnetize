using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour 
{
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			int kickHigh = 2000;
			int kickLow = 1500;

			Rigidbody player = other.GetComponent<Rigidbody>();

			int[] comp = new int[3];
			for(int i=0; i<comp.Length; i++)
			{
				comp[i] =  Random.Range(kickLow, kickHigh);
				if(Random.value < 0.5f) comp[i] *= -1;
			}

			Vector3 force = new Vector3(comp[0], comp[1], comp[2]);
			player.AddForce(force);
			Debug.Log(force);
			this.gameObject.SetActive(false);
		}
	}
}
