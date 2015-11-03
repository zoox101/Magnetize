using UnityEngine;
using System.Collections;

public class MagnetForce : MonoBehaviour {

    public float magnetStrength;
    public float magnetScaling = 1.5f;
    public GameObject player;
    public GameObject magnet;
    public float maxForce;

    private float distance;
    private Vector3 playerDirection;
    private Rigidbody playerBody;
    private float playerMagnetism;
    private Vector3 magnetForce;
    

	void Start()
    {
        playerBody = player.GetComponent<Rigidbody>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            distance = Vector3.Distance(player.transform.position, magnet.GetComponent<Collider>().ClosestPointOnBounds(player.transform.position));
            playerDirection = player.transform.position - magnet.GetComponent<Collider>().ClosestPointOnBounds(player.transform.position);
            playerDirection = Vector3.Normalize(playerDirection);

            magnetForce = magnetStrength * playerDirection / (Mathf.Pow(distance, magnetScaling));
            if (magnetStrength < 0)
                player.GetComponent<TotalForce>().TallyForce(magnetForce * 1);
            else
                player.GetComponent<TotalForce>().TallyForce(magnetForce*-1);

            playerMagnetism = player.GetComponent<PlayerController>().GetPlayerMagnetism();
            magnetForce *= playerMagnetism;

            if (magnetForce.magnitude > maxForce)
                magnetForce = magnetForce.normalized * maxForce;

            playerBody.AddForce(magnetForce);

        }
    }
    
}
