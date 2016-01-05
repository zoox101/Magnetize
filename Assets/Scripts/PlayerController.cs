using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody playerBody;
    public float speed = 50; //Previous default 18

    [SerializeField] private RaycastHit whatIsGround;

	private Material ballMaterial;
	private GameObject camera;
	private float limitedSpeed;
    private int playerMagnetism = -1;
    private bool isGrounded = true;
    private Vector3 movement;
    private Vector3 contactNormal;
    private Vector3 lastContactNormal;
	private GameObject contactObject;

    void Start ()
    {
		Renderer renderer = GetComponent<Renderer> ();
		ballMaterial = renderer.material;
		playerBody = GetComponent<Rigidbody> ();
		camera = GameObject.Find ("Main Camera");
        limitedSpeed = speed / 10;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) this.flip();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = camera.transform.right * moveHorizontal;
        movement += camera.transform.forward * moveVertical;
        movement.Normalize();

        if (isGrounded) playerBody.AddForce(movement * speed);
        else playerBody.AddForce(movement * limitedSpeed);
    }

    void FixedUpdate ()
    {  
        if (playerMagnetism == 1) ballMaterial.color = Color.red;
        else ballMaterial.color = Color.blue;
    }

    void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        contactNormal = other.contacts[0].normal;
		contactObject = other.gameObject;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    public float GetPlayerMagnetism()
    {
        if (playerMagnetism == 1)
            return 1f;
        else
            return -1f;
    }

	public void flip()
	{
		this.playerMagnetism *= -1;
	}

    public bool GetGrounded()
    {
        return isGrounded;
    }

    public Vector3 GetContactNormal()
    {
        return contactNormal;
    }

	public GameObject getContactObject()
	{
		return contactObject;
	}
    
}
