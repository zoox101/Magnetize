using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float cameraDistance;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        //offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
    void Update ()
    {
        offset = player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position);
        offset.Normalize();
        offset *= (6 + player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position).magnitude);
        offset *= -1;
    }
	void LateUpdate () {
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position, Vector3.up);
	}
}
