using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private PlayerController player;

    private void Start()
    {
        // TODO
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (player) {
            Vector3 moveDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
