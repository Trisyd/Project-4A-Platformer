﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] float maxDistance = 3.0f;
    [SerializeField] float enemySpeed = 2.0f;
    private bool isMovingRight = true;
    private float staticPosition;

    // Start is called before the first frame update
    void Start()
    {
        staticPosition = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight == true) { gameObject.transform.Translate(Vector2.right * enemySpeed * Time.deltaTime); }
        else if (isMovingRight == false) { gameObject.transform.Translate(Vector2.left * enemySpeed * Time.deltaTime); }
        if (gameObject.transform.position.x >= maxDistance + staticPosition) { isMovingRight = false; }
        else if (gameObject.transform.position.x <= staticPosition - maxDistance) { isMovingRight = true; }
        //gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
        //if (gameObject.transform.position.x >= maxDistance) { gameObject.transform.Translate(Vector2.left * enemySpeed * Time.deltaTime); }
        //else if (gameObject.transform.position.x <= -maxDistance) { gameObject.transform.Translate(Vector2.right * enemySpeed * Time.deltaTime); }

    }
}
