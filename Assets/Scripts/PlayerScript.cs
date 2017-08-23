using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;
    private Rigidbody2D rb;
    public GameObject trailEffect;

    // Jank variables
    public float jankSpeed = 1f;
    public float jankDrag = 1f;

    // Juice Variables
    public float juiceSpeed = 1f;
    public float juiceDrag = 1f;

    private float moveSpeed;
    public float maxHealth = 100f;
    private float health;
    public float hitCD = 1f;
    private float hitTimer;

    private void Awake()
    {
        // Setup singleton/instance
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Use this for initialization
    private void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        CallJuice(GameController.Instance.isJuicy);
        health = maxHealth;
	}

    // Update called once every fixed amount of time (for Physics)
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            CharacterMovement();
    }

    private void Update()
    {
        if (hitTimer > 0)
            hitTimer -= Time.deltaTime;
    }

    // Physics based character movement
    private void CharacterMovement ()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        rb.AddForce(Vector2.right * (hor * moveSpeed));
        rb.AddForce(Vector2.up * (ver * moveSpeed));
    }

    // Function used to damage this character
    public void TakeDamage (float dmg)
    {
        health -= dmg;

        // If the character is dead, call the fail state
        if (health <= 0)
        {
            GameController.Instance.Fail();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if the wall has been collided with
        if (collision.transform.tag == "Wall" && hitTimer <= 0)
        {
            TakeDamage(collision.relativeVelocity.magnitude);
            GameController.Instance.HitEffects(collision.relativeVelocity.magnitude);
            hitTimer = hitCD;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the goal has been reached
        if (collision.tag == "Goal")
        {
            GameController.Instance.Success();
        }
    }

    // Function that changes juice dependant values that are private
    public void CallJuice (bool isJuice)
    {
        if (isJuice)
        {
            trailEffect.SetActive(true);
            moveSpeed = juiceSpeed;
            rb.drag = juiceDrag;
            rb.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            trailEffect.SetActive(false);
            transform.eulerAngles = Vector3.zero;
            moveSpeed = jankSpeed;
            rb.drag = jankDrag;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public Vector2 GetVelocity ()
    {
        return rb.velocity;
    }

    public float GetHealth01 ()
    {
        return health / maxHealth;
    }
}
