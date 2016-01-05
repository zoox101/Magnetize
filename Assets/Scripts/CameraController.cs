using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float topCamera = 1.2f;
    public float backCamera = 1.35f;
    public float posDamping = 2f;

    private Vector3 offset;
    private float offsetMag;
    private Quaternion newLook;
    private Quaternion oldLook;
    private Vector3 totalForceDirection;
    private Vector3 curryVelocity;
    private PlayerController ballControls;
    
    void Start()
    {
        transform.LookAt(player.transform.position, Vector3.up);
        newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
        ballControls = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        oldLook = newLook;
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

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref curryVelocity, .9f,150f,Time.deltaTime*posDamping);
        totalForceDirection = player.GetComponent<TotalForce>().GetTotalForce();
        totalForceDirection *= -1;
        totalForceDirection.Normalize();

        if (!ballControls.GetGrounded())
        {
            newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
        }
        else
        {
            newLook.SetLookRotation(player.transform.position - transform.position, ballControls.GetContactNormal());
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newLook, Time.deltaTime * Quaternion.Angle(transform.rotation,newLook)*5f);
    }
}
    
