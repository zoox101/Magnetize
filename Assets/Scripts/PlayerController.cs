using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody rb;
    public float speed;
    public Text countText;
    public Text winText;
    public GameObject player;
   	public GameObject camera; //???
    public Material ballMaterial;

    [SerializeField] private RaycastHit whatIsGround;

    private float limitedSpeed;
    private int count;
    private int playerMagnetism;
    private bool isGrounded;
    private Transform groundCheck;
    private float groundedRadius = 5f;
    private Vector3 movement;
    private Vector3 contactNormal;
    private Vector3 lastContactNormal;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        UpdateCountText();
        winText.text = "";
        playerMagnetism = -1;
        limitedSpeed = speed / 10;

        isGrounded = true;
        groundCheck = transform.Find("GroundCheck");

    }
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
			this.flip();
            Debug.Log("Flipped Magnetism");
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = camera.transform.right * moveHorizontal;
        movement += camera.transform.forward * moveVertical;
        movement.Normalize();
        //movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (isGrounded)
        {
            Debug.Log("Grounded");

            rb.AddForce(movement * speed);
        }
        else
        {
            rb.AddForce(movement * limitedSpeed);
        }
    }
    void FixedUpdate ()
    {
        
        if (playerMagnetism == 1)
        {
            ballMaterial.color = Color.red;
        }
        else
        {
            ballMaterial.color = Color.blue;
        }

    }
    void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        //print("Points colliding: " + other.contacts.Length);
        //print("First normal of the points that collide: " + other.contacts[0].normal);
        //lastContactNormal = contactNormal;
        contactNormal = other.contacts[0].normal;
        //contactNormal = player.transform.position - other.transform.position;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            Debug.Log("Deactivated Pickup");
            count += 1;
            UpdateCountText();

        }
    }


    void UpdateCountText ()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 10)
        {
            winText.text = "You Win!";
        }
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
    
}
