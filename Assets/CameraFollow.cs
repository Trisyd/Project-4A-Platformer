using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        //playerGameObject = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y + 1f, -10);
    }
}
