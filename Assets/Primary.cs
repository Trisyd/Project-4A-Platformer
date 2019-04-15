using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primary : MonoBehaviour
{
    [SerializeField] float playerSpeed = 3.0f;
    [SerializeField] float jumpForce = 2.0f;

    private float direction;


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

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
