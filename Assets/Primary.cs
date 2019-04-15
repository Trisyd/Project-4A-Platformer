using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primary : MonoBehaviour
{
    [SerializeField] float playerSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
        }
    }
}
