using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // y-position has a (+1) because I want the camera to sit just above the player
        gameObject.transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y + 1f, -10);
    }
}
