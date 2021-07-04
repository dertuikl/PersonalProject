using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float turnSpeed = 25.0f;

    private float xBound;
    private float zBound;
    private CharacterController controller;

    private float objectWidth => controller.radius * 2;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

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
        controller.Move(moveVector * speed * Time.deltaTime);

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0) {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveVector * speed, turnSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void ConstrainPlayerPosition()
    {
        float xPosCalculated = Mathf.Clamp(transform.position.x, -xBound, xBound);
        float zPosCalculated = Mathf.Clamp(transform.position.z, -zBound, zBound);
        transform.position = new Vector3(xPosCalculated, transform.position.y, zPosCalculated);
    }
}
