using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float cameraDistance;
    public float posDamping;
    private Vector3 offset;
    private Quaternion newLook;
    
    // Use this for initialization
    void Start()
    {
        transform.LookAt(player.transform.position, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        offset = player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position);
        offset += new Vector3(0f, -5, 0);
        offset.Normalize();
        offset *= (8 + player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position).magnitude);
        offset *= -1;
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,player.transform.position + offset,Time.deltaTime*posDamping);
        newLook.SetLookRotation(player.transform.position - transform.position,Vector3.up);
        //transform.LookAt(player.transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newLook, Time.deltaTime*20);
    }
}
    
