using UnityEngine;
using System.Collections;

public class Flip : MonoBehaviour 
{
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			other.GetComponent<PlayerController>().flip();
			this.gameObject.SetActive(false);
		}
	}
}
