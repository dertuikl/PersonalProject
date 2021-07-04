using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float xBound;
    private float zBound;

    private float destroyOffset = 5f;

    private void Start()
    {
        // TODO: unify place where we access this data
        xBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).x - 0.5f) / 2 + destroyOffset;
        zBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).y - 0.5f) / 2 + destroyOffset;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if(transform.position.x < -xBound || transform.position.x > xBound || transform.position.z < -zBound || transform.position.z > zBound){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>()) {
            Debug.Log("Patron hitted enemy " + other.gameObject.name);
        }

        Destroy(gameObject);
    }
}
