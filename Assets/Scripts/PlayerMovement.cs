﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float turnSpeed = 25.0f;

    private float xBound;
    private float zBound;

    private float objectWidth => GetComponent<SphereCollider>().radius * 2;

    private void Start()
    {
        xBound = GameController.Instance.ViewWorldBounds.x - objectWidth;
        zBound = GameController.Instance.ViewWorldBounds.y - objectWidth;
    }

    private void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(moveVector * Time.deltaTime * speed);
        //transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);
    }

    private void ConstrainPlayerPosition()
    {
        float xPosCalculated = Mathf.Clamp(transform.position.x, -xBound, xBound);
        float zPosCalculated = Mathf.Clamp(transform.position.z, -zBound, zBound);
        transform.position = new Vector3(xPosCalculated, transform.position.y, zPosCalculated);
    }
}
