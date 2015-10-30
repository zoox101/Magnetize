using UnityEngine;
using System.Collections;

public class SphereMagnetForce : MonoBehaviour {

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
            distance = Vector3.Distance(player.transform.position, magnet.transform.position);
            playerDirection = player.transform.position - magnet.transform.position;
            playerDirection = Vector3.Normalize(playerDirection);

            playerMagnetism = player.GetComponent<PlayerController>().GetPlayerMagnetism();
            magnetForce = playerMagnetism * magnetStrength * playerDirection / (Mathf.Pow(distance, magnetScaling));

            playerBody.AddForce(magnetForce);

        }
    }
}
