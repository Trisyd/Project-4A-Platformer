using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatformController : MonoBehaviour
{
    [SerializeField] float platformCoefficient = 2f;
    private bool isTouched = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched == true) {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        gameObject.transform.Translate(Vector3.up * platformCoefficient * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player") { isTouched = true; }
    }
}
