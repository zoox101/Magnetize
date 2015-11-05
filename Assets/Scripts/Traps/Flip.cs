using UnityEngine;
using System.Collections;

public class Flip : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			other.GetComponent<PlayerController>().flip();
			this.gameObject.SetActive(false);
		}
	}
}
