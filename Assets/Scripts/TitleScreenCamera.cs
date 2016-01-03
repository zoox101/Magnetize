using UnityEngine;
using System.Collections;

public class TitleScreenCamera : MonoBehaviour 
{
	private int cameraDistance = 20;
	public GameObject reference;
	private Transform targetLocation;

	void Start () 
	{
		targetLocation = reference.transform;
	}
	
	void Update () 
	{
		//transform.Rotate (new Vector3(1 * 15 * direction, speed * 30 * direction, speed * 45 * direction) * Time.deltaTime);
		transform.LookAt (targetLocation.position);

		Quaternion dim4 = targetLocation.rotation;
		Vector3 dim3 = dim4.eulerAngles;

		//Quaternion dim4 = targetLocation.rotation;
		//Vector3 dim3 = dim4.eulerAngles;
		//Vector3 newPos = targetLocation.position + cameraDistance * dim3;
		//transform.position = newPos;
	}
}
