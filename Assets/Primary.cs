using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Primary : MonoBehaviour
{
    private int playerLives = 2;
    private int playerScore = 0;

    [SerializeField] float playerSpeed = 3.0f;
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float contactThreshold = 90f;

    public Text livesText;
    public Text scoreText;
    public Text timeText;

    private float timeMonitoring;

    public Camera camera;

    private Animator animator;

    private float direction;
    private bool grounded = true;
    private bool isAttacking = false;
    private bool hasReversedWeaponCollider = false;
    private bool hasCorrectedWeaponCollider = true;
    //private bool isRunning = false;


    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeMonitoring = 120f - Time.time;
        direction = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector2.left * playerSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else { animator.SetBool("Running", false); }
        if (direction < 0) { GetComponent<SpriteRenderer>().flipX = true; }
        else if (direction > 0) { GetComponent<SpriteRenderer>().flipX = false; }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && grounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)) && grounded == true && !animator.GetBool("Running"))
        {
            isAttacking = true;
            Invoke("TurnOffIsAttacking", 1.0f);
            animator.SetTrigger("Standing&Attacking");
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)) && grounded == true && animator.GetBool("Running")) {
            isAttacking = true;
            Invoke("TurnOffIsAttacking", 1.0f);
            animator.SetTrigger("Running&Attacking"); }

        livesText.text = "Lives: " + playerLives + "/2";
        timeText.text = "Time: " + Mathf.Round(timeMonitoring) + "s";
        scoreText.text = "Score: " + playerScore;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            // I couldn't get this vertex check to implement correctly
            //for (int i = 0; i < collision.contacts.Length; i++)
            //{
            //    if (Vector3.Angle(collision.contacts[i].normal, Vector3.up) <= contactThreshold)
            //    {
            //        Debug.Log("We reached the weird angle code");
            //    }
            //}

            grounded = true;
        }

        else if (collision.transform.tag == "Enemy" && isAttacking)
        {
            Destroy(collision.gameObject);
        }

        else if (collision.transform.tag == "Spike") { playerLives -= 1; }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            Destroy(collision.GetComponent<SpriteRenderer>());
            collision.GetComponent<ParticleSystem>().Play();
            Destroy(collision.gameObject, 1.5f);
            playerScore += 1000;
        }

        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy Hit!");
            playerLives -= 1;
        }
    }

    private void TurnOffIsAttacking() { isAttacking = false; }
}
