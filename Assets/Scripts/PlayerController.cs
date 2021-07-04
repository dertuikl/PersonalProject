using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float turnSpeed = 25.0f;

    private float xBound;
    private float zBound;

    private float objectWidth => GetComponent<SphereCollider>().radius * 2;

    private void Start()
    {
        xBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).x - 0.5f) / 2 - objectWidth;
        zBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).y - 0.5f) / 2 - objectWidth;
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

    private void OnTriggerEnter(Collider other)
    {

    }
}
