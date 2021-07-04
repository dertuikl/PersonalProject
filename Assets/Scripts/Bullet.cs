using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float xBound;
    private float zBound;

    private float destroyOffset = 5f;

    private void Start()
    {
        xBound = GameController.Instance.ViewWorldBounds.x + destroyOffset;
        zBound = GameController.Instance.ViewWorldBounds.y + destroyOffset;
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
            Debug.Log("Bullet hitted enemy " + other.gameObject.name);
        }

        Destroy(gameObject);
    }
}
