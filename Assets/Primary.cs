using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primary : MonoBehaviour
{
    [SerializeField] float playerSpeed = 3.0f;
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float contactThreshold = 90f;

    private Animator animator;

    private float direction;
    private bool grounded = true;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal"));
        direction = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector2.left * playerSpeed * Time.deltaTime);
        }
        if (direction < 0) { GetComponent<SpriteRenderer>().flipX = true; }
        else if (direction > 0) { GetComponent<SpriteRenderer>().flipX = false; }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && grounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetTrigger("Standing&Attacking");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            //for (int i = 0; i < collision.contacts.Length; i++)
            //{
            //    if (Vector3.Angle(collision.contacts[i].normal, Vector3.up) <= contactThreshold)
            //    {
            //        Debug.Log("We reached the weird angle code");
            //    }
            //}

            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            grounded = false;
        }
    }
}
