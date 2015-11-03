using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float topCamera;
    public float backCamera;
    public float posDamping;

    private Vector3 offset;
    private float offsetMag;
    private Quaternion newLook;
    private Vector3 totalForceDirection;
    private Vector3 curryVelocity;
    
    // Use this for initialization
    void Start()
    {
        transform.LookAt(player.transform.position, Vector3.up);
        newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
    }

    // Update is called once per frame
    void Update()
    {
        //offset = player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position);
        //Get force vector
        //theUpVector = player.GetComponent<Rigidbody>().
        //Force vector - normalize - *-5
        //offset += new Vector3(0f, -5, 0);
        //offset.Normalize();
        //offset *= (8 + player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position).magnitude);
        //offset *= -1;
    }
    void LateUpdate()
    {
        offset = player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position);
        offsetMag = offset.magnitude;
        offset = offset.normalized * (backCamera + offsetMag/10);

        offset += player.GetComponent<TotalForce>().GetTotalForce().normalized;
        offset *= (5 + (player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position).magnitude/10));
        offset *= topCamera;
        offset *= -1;

        //transform.position = Vector3.Slerp(transform.position,player.transform.position + offset,Time.deltaTime*posDamping);
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref curryVelocity, .9f,100f,Time.deltaTime);
        //Get total force and position up relative to the opposite of it
        totalForceDirection = player.GetComponent<TotalForce>().GetTotalForce();
        totalForceDirection *= -1;
        totalForceDirection.Normalize();

        newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
        //transform.LookAt(player.transform.position, Vector3.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newLook, Time.deltaTime*20);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newLook, Time.deltaTime * 200);
    }
}
    
