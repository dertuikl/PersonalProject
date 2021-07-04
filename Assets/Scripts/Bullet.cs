using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 20f;
    private float hitPoints = 18f;

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
        if (gameObject.activeInHierarchy) {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if(transform.position.x < -xBound || transform.position.x > xBound || transform.position.z < -zBound || transform.position.z > zBound){
            gameObject.SetActive(false);
        }
    }

    public void SetStats(float moveSpeed, float hitPoints)
    {
        this.moveSpeed = moveSpeed;
        this.hitPoints = hitPoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy) {
            enemy.Hit(hitPoints);
            gameObject.SetActive(false);
        }
    }
}
